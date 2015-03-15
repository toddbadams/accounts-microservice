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
        /// <returns>a account entity</returns>
        public Account ToEntity(long tenantId)
        {
            // todo tba(15/3/15): move DateTime.Now up
            return Account.Create(tenantId, Name, Description, Type, DateTime.Now);
        }
    }
}