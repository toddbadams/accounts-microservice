using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tba.Core.Entities;
using tba.Core.Persistence.Cache;
using tba.Core.Persistence.Extensions;
using tba.Core.Persistence.Interfaces;

namespace tba.CoreUnitTests
{
    [TestClass]
    public class CacheRepositoryUnitTests
    {
        private const long UserId = 666;

        [TestMethod]
        public void CacheRepositoryCanCreate()
        {
            // Arrange
            var entities = FakeEntity.FakeEntities();
            // Act
            var uit = new CacheRepository<Entity>(entities);

            // Assert
            Assert.IsNotNull(uit);
        }

        [TestMethod]
        public async void CacheRepositoryCanInsert()
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

        [TestMethod]
        public async void CacheRepositoryCanUpdate()
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

        [TestMethod]
        public void CacheRepositoryCanDeleteByQuery()
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

        [TestMethod]
        public void CacheRepositoryCanDeleteById()
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