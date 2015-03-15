using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tba.Core.Persistence.Cache;
using tba.Core.Persistence.Extensions;
using tba.Core.Persistence.Interfaces;

namespace tba.CoreUnitTests
{
    [TestClass]
    public class CacheReadOnlyRepositoryUnitTests
    {
        [TestMethod]
        public void CacheReadOnlyRepository_ShouldCreate()
        {
            // Arrange
            var entities = FakeEntity.FakeEntities();

            // Act
            var uit = new CacheReadOnlyRepository<FakeEntity>(entities);

            // Assert
            Assert.IsNotNull(uit);
        }

        [TestMethod]
        public void CacheReadOnlyRepository_ShouldFetchAll()
        {
            // Arrange
            var entities = FakeEntity.FakeEntities();
            IReadOnlyRepository<FakeEntity> repository = new CacheReadOnlyRepository<FakeEntity>(entities);

            // Act
            var result = repository.Query().ToArray();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entities.Count, result.Length);
        }

        [TestMethod]
        public void CacheReadOnlyRepository_ShouldFetchByQuery()
        {
            // Arrange
            var entities = FakeEntity.FakeEntities();

            IReadOnlyRepository<FakeEntity> repository = new CacheReadOnlyRepository<FakeEntity>(entities);

            // Act
            var result = repository.Query().IsNotDeleted().ToArray();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entities.Count, result.Length);
        }

        [TestMethod]
        public void CacheReadOnlyRepository_ShouldCheckExistenceById()
        {
            // Arrange
            const int idNotUsed = 555;
            var entities = FakeEntity.FakeEntities();
            var entity = entities.First();
            IReadOnlyRepository<FakeEntity> repository = new CacheReadOnlyRepository<FakeEntity>(entities);

            // Act
            var resultShouldBeTrue = repository.Exists(entity.Id);
            var resultShouldBeFalse = repository.Exists(idNotUsed);

            // Assert
            Assert.IsTrue(resultShouldBeTrue);
            Assert.IsFalse(resultShouldBeFalse);
        }

        [TestMethod]
        public void CacheReadOnlyRepository_ShouldGetById()
        {
            // Arrange
            var entities = FakeEntity.FakeEntities();
            var entity = entities.First();
            IReadOnlyRepository<FakeEntity> repository = new CacheReadOnlyRepository<FakeEntity>(entities);

            // Act
            var result = repository.Get(entity.Id);

            // Assert
            Assert.AreEqual(entity, result);
        }
    }
}