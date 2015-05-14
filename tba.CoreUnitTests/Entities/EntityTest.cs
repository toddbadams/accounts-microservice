using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace tba.CoreUnitTests.Entities
{
    [TestClass]
    public class EntityTest
    {
        [TestMethod]
        public void Should_Be_Equal_With_Same_Ids()
        {
            // Arrange
            var e1 = new FakeEntity { Id = 1, TenantId = 99 };
            var e2 = new FakeEntity { Id = 1, TenantId = 888 };

            // Act
            var isEqual = e1 == e2;

            // Assert
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void Should_Not_Be_Equal_With_Different_Ids()
        {
            // Arrange
            var e1 = new FakeEntity { Id = 1, TenantId = 99 };
            var e2 = new FakeEntity { Id = 2, TenantId = 888 };

            // Act
            var isEqual = e1 == e2;

            // Assert
            Assert.IsFalse(isEqual);
        }
    }
}
