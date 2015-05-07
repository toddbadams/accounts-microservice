using Microsoft.VisualStudio.TestTools.UnitTesting;
using tba.SelfHost;

namespace tba.AccountsWebApi.Tests.Controllers
{
    [TestClass]
    public class AccountsControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            var controller = new AccountsController();

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
        }


    }
}
