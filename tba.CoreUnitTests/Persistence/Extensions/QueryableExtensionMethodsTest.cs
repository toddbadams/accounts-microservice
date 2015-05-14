using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tba.Core.Persistence.Extensions;
using tba.CoreUnitTests.Entities;

namespace tba.CoreUnitTests.Persistence.Extensions
{
    [TestClass]
    public class QueryableExtensionMethodsTest
    {
        [TestMethod]
        public void Tenant_Should_FilterByTenantId()
        {
            // Arrange
            var entities = FakeEntity.FakeEntities();
            var query = entities.AsQueryable();

            // Act
            var results = query.Tenant(1);

            // Assert
            Assert.AreEqual(2, results.Count());
        }
    }
}
