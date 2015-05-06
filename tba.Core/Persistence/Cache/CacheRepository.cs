using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tba.Core.Entities;
using tba.Core.Persistence.Interfaces;

namespace tba.Core.Persistence.Cache
{
    /// <summary>
    /// Cache based read-write repository.
    /// No use of audit trial in cache based repository.
    /// </summary>
    public sealed class CacheRepository<T> : CacheReadOnlyRepository<T>, IRepository<T> where T : Entity
    {

        /// <summary>
        ///     Constructor to create cache repository from a collection of IEntity objects
        /// </summary>
        /// <param name="items">IEntity items in the cache repository</param>
        public CacheRepository(IEnumerable<T> items)
            : base(items)
        {
        }

        #region public CRUD Methods

        /// <summary>
        ///     Create an entity in the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="entity">entity to create</param>
        public Task InsertAsync(long userId, T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            // add entity to data store
            Items.Add(entity);
            return null;
        }

        /// <summary>
        ///     Update an entity in the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="entity">entity to update</param>
        public Task UpdateAsync(long userId, T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            // update to data store
            Delete(userId, entity.Id);
            Items.Add(entity);
            return null;
        }

        /// <summary>
        ///     Update entities in the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="entities">enumerable collection of entities to update</param>
        public Task UpdateAsync(long userId, IQueryable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            // process unit of work prior to update in data store
            foreach (var entity in entities)
            {
                UpdateAsync(userId, entity);
            }
            return null;
        }

        /// <summary>
        ///     Delete an entities that match the query from the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="entities">entities to delete</param>
        public void Delete(long userId, IQueryable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            // loop and delete
            var items = entities.ToArray();
            foreach(var item in items)
            {
                Delete(userId, item.Id);
            }
        }

        /// <summary>
        ///     Delete an entities that match the domainFilter from the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="id">id of the record</param>
        public void Delete(long userId, long id)
        {
            // get our query 
            var entity = Get(id);

            //  delete
            Items.Remove(entity);
        }

        #endregion
    }
}