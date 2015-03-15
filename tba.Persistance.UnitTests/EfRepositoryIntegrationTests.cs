using Microsoft.VisualStudio.TestTools.UnitTesting;
using scheduler.Core.Entities;
using scheduler.Persistence;

namespace scheduler.Persistance.UnitTests
{
    [TestClass]
    public class EfRepositoryIntegrationTests
    {
        [TestMethod]
        public void EfRepository_ShouldCreate()
        {
            // Arrange
            var efContext = new EfContext();

            var entity = new Entity
            {
                Id = 1,
            };

            var repository = new EfRepository<Entity>(efContext);

            // Act
            repository.Insert(entity);

            // Assert
            Assert.IsNotNull(repository);
        }


        [TestMethod]
        public void EfRepository_ShouldInsert()
        {
            // Arrange
            var efContext = new EfContext();

            var entities = FakeEntity.FakeEntities();

            var repository = new EfRepository<Entity>(efContext);

            // Act
            foreach (var item in entities)
            {
                repository.Insert(item);
            }

            // Assert
            Assert.IsNotNull(repository);
        }

    }
}