using Microsoft.VisualStudio.TestTools.UnitTesting;
using tba.Accounts.Controllers;

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
