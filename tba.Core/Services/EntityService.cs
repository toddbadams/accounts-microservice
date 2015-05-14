using System;
using System.Data.Entity;
using System.Threading.Tasks;
using log4net;
using tba.Core.Entities;
using tba.Core.Exceptions;
using tba.Core.Persistence.Extensions;
using tba.Core.Persistence.Interfaces;
using tba.Core.Utilities;

namespace tba.Core.Services
{
    public abstract class EntityService<T> : IDisposable
        where T : Entity
    {
        protected readonly IRepository<T> Repository;
        protected readonly string FriendlyName;
        protected readonly ITimeProvider TimeProvider;
        protected readonly ILog Log;

        protected EntityService(IRepository<T> repository, string serviceName, string friendlyName, ITimeProvider timeProvider)
        {
            Repository = repository;
            Log = LogManager.GetLogger(serviceName);
            FriendlyName = friendlyName;
            TimeProvider = timeProvider;
        }

        #region Public Methods
        /// <summary>
        /// Delete an existing account
        /// </summary>
        /// <param name="tenantId">a tenant</param>
        /// <param name="userId">a user</param>
        /// <param name="id">the id of the account to delete</param>
        public async Task DeleteAsync(long tenantId, long userId, long id)
        {
            await SetIsDeleted(tenantId, userId, id, true);
        }

        /// <summary>
        /// Dispose this service
        /// </summary>
        public void Dispose()
        {
            Repository.Dispose();
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Fetch an array of entities
        /// </summary>
        /// <param name="tenantId">a tenant</param>
        /// <param name="userId">a user</param>
        /// <param name="parentId">OPTIONAL - a parent entity</param>
        /// <param name="isDeleted">true to delete, false to undelete</param>
        /// <returns>an array of entities</returns>
        protected async Task<T[]> FetchEntitiesAsync(long tenantId, long userId, long? parentId, bool isDeleted)
        {
            var msg = "FetchEntitiesAsync" + ". " +
                       string.Format("tenantId={0}, userId={1}, parentId={2}", tenantId, userId, parentId);
            try
            {
                Log.Debug(msg);
                var query = Repository.Query();
                query = isDeleted ? query.IsDeleted() : query.IsNotDeleted();
                query = query.Tenant(tenantId);
                if (query == null) throw new Exception("empty query");
                var es = await query.ToArrayAsync();
                Log.Debug(msg + " => " + Serialization.Serialize(es));
                return es;
            }
            catch (Exception exception)
            {
                Log.Error(msg, exception);
                throw;
            }
        }

        /// <summary>
        /// Get a single entity
        /// </summary>
        /// <param name="tenantId">a tenant</param>
        /// <param name="userId">a user</param>
        /// <param name="entityId">Id of the entity to delete or undelete</param>
        /// <returns>the entity</returns>
        protected T GetEntity(long tenantId, long userId, long entityId)
        {
            var msg = "SetIsDeleted" + ". " +
                       string.Format("tenantId={0}, userId={1}, entityId={2}", tenantId, userId, entityId);
            try
            {
                // multi-tennant security check, task must exists for the client.
                if (!Exists(tenantId, entityId))
                {
                    var m = "The requested" + FriendlyName + " does not exist.";
                    Log.Error(msg);
                    throw new ApplicationException(m);
                }
                Log.Debug(msg);
                var e = Repository.Get(entityId);
                Log.Debug(msg + " => " + Serialization.Serialize(e));

                return e;
            }
            catch (Exception exception)
            {
                Log.Error(msg, exception);
                return null;
            }
        }

        /// <summary>
        /// Verify if an entity exists for the given tenant
        /// </summary>
        /// <param name="tenantId">a tenant</param>
        /// <param name="entityId">Id of the entity to delete or undelete</param>
        /// <param name="isDeleted">OPTIONAL if true check for deleted entities, else check not deleted entities</param>
        /// <returns>true if the entity exists within the tenant, else false</returns>
        protected bool Exists(long tenantId, long entityId, bool isDeleted = false)
        {
            var q = Repository.Query()
                .Tenant(tenantId);
            q = isDeleted ? q.IsDeleted() : q.IsNotDeleted();

            return q.IdExists(entityId);
        }

        /// <summary>
        /// multi-tennant security check, account must exists for the tenant.
        /// </summary>
        /// <param name="tenantId">a tenant</param>
        /// <param name="entityId">Id of the entity to delete or undelete</param>
        /// <param name="msg">Error message</param>
        protected void TenantCheck(long tenantId, long entityId, string msg)
        {
            if (Exists(tenantId, entityId)) return;
            var m = string.Format("The {0} with id={1} submitted for update does not exist.", FriendlyName, entityId);
            Log.Error(msg);
            throw new EntityDoesNotExistException(m);
        }

        /// <summary>
        /// Set an entity to deleted or NOT deleted
        /// </summary>
        /// <param name="tenantId">a tenant</param>
        /// <param name="userId">a user</param>
        /// <param name="entityId">Id of the entity to delete or undelete</param>
        /// <param name="isDeleted">true to delete, false to undelete</param>
        /// <returns>The updated entity</returns>
        protected async Task SetIsDeleted(long tenantId, long userId, long entityId, bool isDeleted)
        {
            var msg = "SetIsDeleted" + ". " +
                       string.Format("tenantId={0}, userId={1}, entityId={2}, active={3}", tenantId, userId, entityId, isDeleted);
            try
            {
                TenantCheck(tenantId, entityId, msg);
                Log.Debug(msg);
                var e = Repository.Get(entityId);
                e.IsDeleted = isDeleted;
                await Repository.UpdateAsync(userId, e);
                Log.Debug(msg + " => " + Serialization.Serialize(e));
            }
            catch (EntityDoesNotExistException exception)
            {
                Log.Error(msg, exception);
                throw;
            }
            catch (Exception exception)
            {
                Log.Error(msg, exception);
                throw new ApplicationException("Failed to " + (isDeleted ? "activate" : "deactive") + " " + FriendlyName);
            }
        }
        #endregion
    }
}