using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public Task<T> InsertAsync(long userId, T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            var task = Task.Factory.StartNew(() =>
            {
                // add entity to data store
                Items.Add(entity);
                return entity;
            });

            return task;
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
            var task = Task.Factory.StartNew(() =>
            {
                // update to data store
                Delete(userId, entity.Id);
                Items.Add(entity);
                return entity;
            });

            return await task;
        }

        /// <summary>
        ///     Update entities in the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="entities">enumerable collection of entities to update</param>
        public async Task<IEnumerable<T>> UpdateAsync(long userId, IQueryable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            var task = Task.Factory.StartNew(() => Update(userId, entities));

            return await task;
        }

        private IEnumerable<T> Update(long userId, IQueryable<T> entities)
        {
            var results = new List<T>();
            foreach (var entity in entities)
            {
                // update to data store
                Delete(userId, entity.Id);
                Items.Add(entity);
                results.Add(entity);
            }
            return results;
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
            foreach (var item in items)
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