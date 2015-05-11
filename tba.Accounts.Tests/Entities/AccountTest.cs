using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tba.Accounts.Entities;

namespace tba.AccountsWebApi.Tests.Entities
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void Account_ShouldCreate()
        {
            // Arrange
            const long tenantId = 99;
            const string name = "name";
            const string description = "description";
            const Account.AccountTaxonomy accountTaxonomy = Account.AccountTaxonomy.Current;
            var openedDateTime = DateTime.Now;

            // Act
            var result = Account.Create(tenantId, name, description, accountTaxonomy, openedDateTime);

            // Asset
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Id);
            Assert.AreEqual(tenantId, result.TenantId);
            Assert.IsFalse(result.IsDeleted);
            Assert.AreEqual(name, result.Name);
            Assert.AreEqual(description, result.Description);
            Assert.AreEqual(accountTaxonomy, result.Type);
            Assert.AreEqual(openedDateTime, result.OpenDate);
            Assert.IsNull(result.CloseDate);
        }
    }
}
