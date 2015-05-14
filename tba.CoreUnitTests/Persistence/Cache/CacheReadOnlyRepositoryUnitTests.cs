using System.Linq;
using NUnit.Framework;
using tba.Core.Persistence.Cache;
using tba.Core.Persistence.Extensions;
using tba.Core.Persistence.Interfaces;
using tba.CoreUnitTests.Entities;

namespace tba.CoreUnitTests.Persistence.Cache
{
    [TestFixture]
    public class CacheReadOnlyRepositoryUnitTests
    {
        [Test]
        public void Should_Create()
        {
            // Arrange
            var entities = FakeEntity.FakeEntities();

            // Act
            var uit = new CacheReadOnlyRepository<FakeEntity>(entities);

            // Assert
            Assert.IsNotNull(uit);
        }

        [Test]
        public void Should_FetchAll()
        {
            // Arrange
            var entities = FakeEntity.FakeEntities();
            var count = entities.Count();
            IReadOnlyRepository<FakeEntity> repository = new CacheReadOnlyRepository<FakeEntity>(entities);

            // Act
            var result = repository.Query().ToArray();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(count, result.Length);
        }

        [Test]
        public void Should_FetchByQuery()
        {
            // Arrange
            var entities = FakeEntity.FakeEntities();
            var count = entities.Count();
            IReadOnlyRepository<FakeEntity> repository = new CacheReadOnlyRepository<FakeEntity>(entities);

            // Act
            var result = repository.Query().IsNotDeleted().ToArray();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(count, result.Length);
        }

        [Test]
        public void Should_CheckExistenceById()
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

        [Test]
        public void Should_GetById()
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