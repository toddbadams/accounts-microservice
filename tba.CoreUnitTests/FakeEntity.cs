using System.Collections.Generic;
using tba.Core.Entities;

namespace tba.CoreUnitTests
{
    public class FakeEntity : Entity
    {
        public static List<FakeEntity> FakeEntities()
        {
            var entities = new List<FakeEntity>
            {
                new FakeEntity
                {
                    Id = 1
                },
                new FakeEntity
                {
                    Id = 2
                },
                new FakeEntity
                {
                    Id = 3
                }
            };
            return entities;
        }
    }
}