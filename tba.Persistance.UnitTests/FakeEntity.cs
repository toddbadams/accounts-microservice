using System.Collections.Generic;
using scheduler.Core.Entities;

namespace scheduler.Persistance.UnitTests
{
    public static class FakeEntity 
    {
        public static List<Entity> FakeEntities()
        {
            var entities = new List<Entity>
            {
                new Entity
                {
                    Id = 1,
                },
                new Entity
                {
                    Id = 2,
                },
                new Entity
                {
                    Id = 3,
                }
            };
            return entities;
        }
    }
}