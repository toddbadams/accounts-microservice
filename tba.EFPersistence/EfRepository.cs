using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using tba.Core.Entities;
using tba.Core.Persistence.Interfaces;
using tba.Core.Utilities;

namespace tba.EFPersistence
{
    /// <summary>
    ///     Entity Framework based read-write repository.
    /// </summary>
    public sealed class EfRepository<T> : EfReadOnlyRepository<T>, IRepository<T> where T : Entity
    {
        private readonly ITimeProvider _timeProvider;
        private readonly IDbSet<Entity.Audit> _auditTable;

        public EfRepository(DbContext context, ITimeProvider timeProvider)
            : base(context)
        {
            _timeProvider = timeProvider;
            _auditTable = context.Set<Entity.Audit>();
        }

        #region public CRUD Methods

        /// <summary>
        ///     Create an entity in the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="entity">entity to create</param>
        public async Task<T> InsertAsync(long userId, T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            // add entity to data store
            Table.Add(entity);

            Audit(userId, entity, Entity.AuditActionType.Insert);

            // update the data store
            await Context.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        ///     Update an entity in the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="entity">entity to update</param>
        public async Task<T> UpdateAsync(long userId, T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Audit(userId, entity, Entity.AuditActionType.Update);

            // update to data store
            Table.Attach(entity);
            Context.Entry((Entity)entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        ///     Update entities in the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="entities">entities to update</param>
        public async Task<IEnumerable<T>> UpdateAsync(long userId, IQueryable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            // process unit of work prior to update in data store
            foreach (var entity in entities)
            {
                Audit(userId, entity, Entity.AuditActionType.Update);
                Table.Attach(entity);
                Context.Entry((Entity)entity).State = EntityState.Modified;
            }

            // update to data store  
            await Context.SaveChangesAsync();
            return entities.AsEnumerable();
        }

        /// <summary>
        ///     Delete an entities that match the domainFilter from the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="entities">entities to delete</param>
        public void Delete(long userId, IQueryable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            // loop and delete
            foreach (T entity in entities)
            {
                Audit(userId, entity, Entity.AuditActionType.Delete);

                if (Context.Entry(entity).State == EntityState.Detached)
                    Table.Attach(entity);

                Table.Remove(entity);
            }

            // comit the changes
            Context.SaveChanges();
        }

        /// <summary>
        ///     Delete an entities that match the domainFilter from the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="id">id of the record</param>
        public void Delete(long userId, long id)
        {
            //if (id == null || id == long.Empty)
            //    throw new ArgumentException("id");

            // get our entity
            T entity = Table.Find(id);

            Audit(userId, entity, Entity.AuditActionType.Delete);

            //  delete
            if (Context.Entry(entity).State == EntityState.Detached)
                Table.Attach(entity);
            Table.Remove(entity);

            // comit the changes
            Context.SaveChanges();
        }
        #endregion

        /// <summary>
        /// Provide an audit trail for unsafe actions
        /// </summary>
        /// <param name="userId">the user making the change</param>
        /// <param name="entity">the entity being changed</param>
        /// <param name="auditAuditAction">The change AuditAction (insert,update,delete)</param>
        private void Audit(long userId, T entity, Entity.AuditActionType auditAuditAction)
        {
            var a = entity.ToAuditEntity(userId, _timeProvider.UtcNow.ToUnixTimestamp(), auditAuditAction);
            _auditTable.Add(a);
        }
    }
}