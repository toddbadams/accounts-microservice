using System.Linq;
using System.Threading.Tasks;
using tba.Core.Entities;

namespace tba.Core.Persistence.Interfaces
{
    /// <summary>
    ///     Represents a read/write repository of IRna objects.
    /// </summary>
    public interface IRepository<T> : IReadOnlyRepository<T> where T : Entity
    {
        /// <summary>
        ///     Create an entity in the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="entity">entity to create</param>
        Task InsertAsync(long userId, T entity);

        /// <summary>
        ///     Update an entity in the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="entity">entity to update</param>
        Task UpdateAsync(long userId, T entity);

        /// <summary>
        ///     Update entities in the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="entities">enumerable collection of entities to update</param>
        Task UpdateAsync(long userId, IQueryable<T> entities);

        /// <summary>
        ///     Delete an entities that match the domainFilter from the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="domainFilter">domainFilter of entities to delete</param>
        void Delete(long userId, IQueryable<T> domainFilter);

        /// <summary>
        ///     Delete an entities that match the domainFilter from the data store
        /// </summary>
        /// <param name="userId">user id used to capture in audit</param>
        /// <param name="id">id of the record</param>
        void Delete(long userId, long id);
    }
}