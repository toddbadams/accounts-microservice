using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tba.Core.Entities;

namespace tba.Accounts.Entities
{
    public class Account : Entity
    {
        /// <summary>
        /// A 512 character note describing the account
        /// </summary>
        [MaxLength(512)]
        public string Description { get; set; }

        /// <summary>
        /// A 64 character account name
        /// </summary>
        [MaxLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// The type of account
        /// </summary>
        [Required]
        public AccountTaxonomy Type { get; set; }

        /// <summary>
        ///  Date the account was opened
        /// </summary>
        [Required]
        public DateTime OpenDate { get; set; }

        /// <summary>
        ///  If closed the date the account was closed (null if currently open)
        /// </summary>
        public DateTime? CloseDate { get; set; }

        public enum AccountTaxonomy
        {
            Current = 1012334,          // System taxonomy for Current account types
            Personal = 1012335,         // System taxonomy for Personal account types
            Isa = 1012336,              // System taxonomy for ISA account types
            Tessa = 1012337,            // System taxonomy for TESSA account types
            MoneyMarket = 1012338       // System taxonomy for Money Market account types
        }

        public static Account Create(long tenantId, string name, string description, AccountTaxonomy accountAccountTaxonomy, DateTime openedDateTime)
        {
            var e = new Account
            {
                TenantId = tenantId,
                Name = name,
                Description = description,
                Type = accountAccountTaxonomy,
                OpenDate = openedDateTime
            };
            return e;
        }

        public static IDictionary<AccountTaxonomy, string> TypeName = new Dictionary<AccountTaxonomy, string>
        {
            { AccountTaxonomy.Current, "Current Account"},
            { AccountTaxonomy.Isa, "ISA Account"},
            { AccountTaxonomy.MoneyMarket, "Money Market Account"},
            { AccountTaxonomy.Personal, "Personal Account"},
            { AccountTaxonomy.Tessa, "TESSA Account"},
        };

        /// <summary>
        /// Close an account
        /// </summary>
        /// <param name="timestamp"></param>
        public void CloseAccount(DateTime timestamp)
        {
            CloseDate = timestamp;
        }
    }
}