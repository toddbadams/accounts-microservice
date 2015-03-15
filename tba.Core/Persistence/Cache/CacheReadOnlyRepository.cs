using System;
using System.Collections.Generic;
using System.Linq;
using tba.Core.Entities;
using tba.Core.Persistence.Interfaces;

namespace tba.Core.Persistence.Cache
{
    /// <summary>
    ///     Cache based read-only repository.
    /// </summary>
    public class CacheReadOnlyRepository<T> : IReadOnlyRepository<T> where T : Entity
    {
        /// <summary>
        ///     Constructor to create cache repository from a collection of IEntity objects
        /// </summary>
        /// <param name="items">IEntity items in the cache repository</param>
        public CacheReadOnlyRepository(IEnumerable<T> items)
        {
            Items = items.Cast<Entity>().ToList();
        }

        #region Protected Properties and methods
        protected IQueryable<Entity> Table { get { return Items.AsQueryable(); } }
        protected readonly IList<Entity> Items;
        #endregion

        #region Private Vars
        private bool _disposed; // track if disposed
        #endregion

        #region public CRUD Methods

        /// <summary>
        ///     Query from the data store
        /// </summary>
        /// <returns>List of entites</returns>
        public IQueryable<T> Query()
        {
            // return list
            return Table.Cast<T>();
        }

        /// <summary>
        ///     check for the existance a single record from the data store
        /// </summary>
        /// <param name="id">id of the record</param>
        /// <returns>returns first or default entity</returns>
        public bool Exists(long id)
        {
            return Table.Any(i => i.Id == id);
        }

        /// <summary>
        ///     Get a single record from the data store
        /// </summary>
        /// <param name="id">id of the record</param>
        /// <returns>returns first or default entity</returns>
        public T Get(long id)
        {
            //    if (id == null || id == long.Empty)
            //        throw new ArgumentException("id");

            // get our query 
            var item = Table.OfType<T>().First(t => t.Id == id);
            if (item == null)
            {
                throw new KeyNotFoundException("entity");
            }

            // return list
            return item;
        }

        #endregion

        #region Dispose

        /// <summary>
        ///     dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     dispose
        /// </summary>
        /// <param name="disposing">are we currently disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // what to do here??
                }
            }
            _disposed = true;
        }

        #endregion
    }
}