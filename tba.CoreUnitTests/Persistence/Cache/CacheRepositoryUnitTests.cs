using System.Linq;
using NUnit.Framework;
using tba.Core.Entities;
using tba.Core.Persistence.Cache;
using tba.Core.Persistence.Extensions;
using tba.Core.Persistence.Interfaces;
using tba.CoreUnitTests.Entities;

namespace tba.CoreUnitTests.Persistence.Cache
{
    [TestFixture]
    public class CacheRepositoryUnitTests
    {
        private const long UserId = 666;

        [Test]
        public void Should_Create()
        {
            // Arrange
            var entities = FakeEntity.FakeEntities();
            // Act
            var uit = new CacheRepository<Entity>(entities);

            // Assert
            Assert.IsNotNull(uit);
        }

        [Test]
        public async void Should_Insert()
        {
            // Arrange
            var entities = FakeEntity.FakeEntities();
            var newEntity = new FakeEntity
            {
                Id = 9
            };

            var repository = new CacheRepository<FakeEntity>(entities);

            // Act
            await repository.InsertAsync(UserId, newEntity);
            var result = repository.Exists(newEntity.Id);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async void Should_Update()
        {
            // Arrange
            var entities = FakeEntity.FakeEntities();
            var entity = entities.First();
            entity.IsDeleted = true;
            var repository = new CacheRepository<FakeEntity>(entities);

            // Act
            await repository.UpdateAsync(UserId, entity);
            var result = repository.Get(entity.Id);

            // Assert
            Assert.AreEqual(entity, result);
        }

        [Test]
        public void Should_DeleteByQuery()
        {
            // Arrange
            var entities = FakeEntity.FakeEntities();
            IRepository<FakeEntity> repository = new CacheRepository<FakeEntity>(entities);
            var query = repository.Query().IsNotDeleted();

            // Act
            repository.Delete(UserId, query);

            // Assert
            foreach (var entity in entities)
            {
                Assert.IsFalse(repository.Exists(entity.Id));
            }
        }

        [Test]
        public void Should_DeleteById()
        {
            // Arrange
            var entities = FakeEntity.FakeEntities();
            var entity = entities.First();
            var repository = new CacheRepository<FakeEntity>(entities);

            // Act
            repository.Delete(UserId, entity.Id);

            // Assert
            Assert.IsFalse(repository.Exists(entity.Id));
        }
    }
}