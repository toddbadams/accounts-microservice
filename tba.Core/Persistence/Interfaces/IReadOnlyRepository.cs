using System;
using System.Linq;
using tba.Core.Entities;

namespace tba.Core.Persistence.Interfaces
{
    /// <summary>
    ///     Represent an readonly repository of IRna objects.
    /// </summary>
    public interface IReadOnlyRepository<out T> : IDisposable where T : Entity
    {
        /// <summary>
        ///     Query from the data store
        /// </summary>
        /// <returns>List of entites</returns>
        IQueryable<T> Query();

        /// <summary>
        ///     check for the existance a single record from the data store
        /// </summary>
        /// <param name="id">id of the record</param>
        /// <returns>returns first or default entity</returns>
        bool Exists(long id);

        /// <summary>
        ///     Get a single record from the data store
        /// </summary>
        /// <param name="id">id of the record</param>
        /// <returns>returns first or default entity</returns>
        T Get(long id);
    }
}