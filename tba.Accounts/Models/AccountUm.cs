using System;
using tba.Accounts.Entities;

namespace tba.accounts.Models
{
    /// <summary>
    /// A account update view model
    /// </summary>
    public class AccountUm
    {
        public string Description { get; set; }

        /// <summary>
        /// A mapper from this account update model to a account entity
        /// </summary>
        /// <param name="entity">a account entity</param>
        public void UpdateEntity(Account entity)
        {
            // only update description if not null
            if (!string.IsNullOrEmpty(Description))
            {
                entity.Description = Description;
            }
        }
    }
}