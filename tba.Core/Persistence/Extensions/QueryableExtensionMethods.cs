using System.Linq;
using tba.Core.Entities;

namespace tba.Core.Persistence.Extensions
{
    public static class QueryableExtensionMethods
    {
        /// <summary>
        /// Return entities with a given tenant
        /// </summary>
        public static IQueryable<T> Tenant<T>(this IQueryable<T> query, long tenantId) where T : Entity
        {
            return query
                .Where(item => item.TenantId == tenantId);
        }

        /// <summary>
        /// Return a paged set of entities
        /// </summary>
        public static IQueryable<T> Paged<T>(this IQueryable<T> query, int start, int limit) where T : Entity
        {
            return query
                .Skip(start - 1)
                .Take(limit);
        }

        /// <summary>
        /// Return only active entities
        /// </summary>
        public static IQueryable<T> IsDeleted<T>(this IQueryable<T> query) where T : Entity
        {
            return query
                .Where(item => item.IsDeleted);
        }

        /// <summary>
        /// Return only active entities
        /// </summary>
        public static IQueryable<T> IsNotDeleted<T>(this IQueryable<T> query) where T : Entity
        {
            return query
                .Where(item => !item.IsDeleted);
        }

        /// <summary>
        /// Check if an id exists
        /// </summary>
        public static bool IdExists<T>(this IQueryable<T> query, long id) where T : Entity
        {
            var result = query.Any(item => item.Id == id);
            return result;
        }


        /// <summary>
        /// Return entities with a given tenant
        /// </summary>
        public static IQueryable<T> Email<T>(this IQueryable<T> query, string email) where T : IEmail
        {
            return query
                .Where(item => item.Email == email);
        }
    }
}