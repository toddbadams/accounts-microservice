using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tba.accounts.Models;
using tba.Accounts.Entities;

namespace tba.AccountsWebApi.Tests.Models
{
    [TestClass]
    public class AccountCmTest
    {
        [TestMethod]
        public void Should_CreateNewAccountEntity()
        {
            // Arrange
            const long tenantId = 99;
            var openedDateTime = DateTime.Now;
            var uit = new AccountCm
            {
                Description = "description",
                Name = "name",
                Type = Account.AccountTaxonomy.Current
            };

            // Act
            var result = uit.ToEntity(tenantId, openedDateTime);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Id);
            Assert.AreEqual(tenantId, result.TenantId);
            Assert.IsFalse(result.IsDeleted);
            Assert.AreEqual(uit.Name, result.Name);
            Assert.AreEqual(uit.Description, result.Description);
            Assert.AreEqual(uit.Type, result.Type);
            Assert.AreEqual(openedDateTime, result.OpenDate);
            Assert.IsNull(result.CloseDate);
        }
    }
}
