using System;
using System.Collections.Generic;
using tba.Core.Entities;

namespace tba.CoreUnitTests.Entities
{
    public class FakeEntity : Entity
    {
        public static Func<IEnumerable<FakeEntity>> FakeEntities = (() => new List<FakeEntity>
        {
            new FakeEntity
            {
                Id = 1,
                TenantId = 1
            },
            new FakeEntity
            {
                Id = 2,
                TenantId = 2
            },
            new FakeEntity
            {
                Id = 3,
                TenantId = 1
            }
        });
    }
}