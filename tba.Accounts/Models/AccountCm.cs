using System;
using tba.Accounts.Entities;

namespace tba.accounts.Models
{
    /// <summary>
    /// A create a new Account model
    /// </summary>
    public class AccountCm
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public Account.AccountTaxonomy Type { get; set; }

        /// <summary>
        /// Mapper from this create account model to a account entity
        /// </summary>
        /// <param name="tenantId">the tenant</param>
        /// <param name="openDate">Date of account open</param>
        /// <returns>a account entity</returns>
        public Account ToEntity(long tenantId, DateTime openDate)
        {
            return Account.Create(tenantId, Name, Description, Type, openDate);
        }
    }
}