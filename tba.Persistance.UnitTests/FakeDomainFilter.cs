using System.Linq;
using tba.Core.Entities;

namespace tba.Persistance.UnitTests
{
    public class FakeDomainFilter : IDomainFilter<Entity>
    {
        public IQueryable BuildQueryFrom(IQueryable<Entity> query)
        {
            return query;
        }
    }
}