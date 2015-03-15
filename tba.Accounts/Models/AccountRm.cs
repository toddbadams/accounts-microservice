using System.Collections.Generic;
using System.Linq;
using tba.Accounts.Entities;
using tba.Core.Utilities;
using tba.Core.ViewModels;

namespace tba.accounts.Models
{
    /// <summary>
    /// A read-only view model for a single account
    /// </summary>
    public class AccountRm : ReadOnlyViewModel
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public AccountTypeRm Type { get; set; }
        public long OpenDate { get; set; }
        public long? CloseDate { get; set; }

        public class AccountTypeRm
        {
            public long Id { get; set; }
            public string Name { get; set; }
        }

        /// <summary>
        /// Map from a account entity to a account read-only view model
        /// </summary>
        /// <param name="entity">a account entity</param>
        /// <returns>a account read-only view model</returns>
        internal static AccountRm From(Account entity)
        {
            return entity == null
                ? null
                : new AccountRm
                {
                    Id = entity.Id,
                    Description = entity.Description ?? string.Empty,
                    Name = entity.Name,
                    Type = new AccountTypeRm
                    {
                        Id = (long)entity.Type,
                        Name = Account.TypeName[entity.Type]
                    },
                    OpenDate = entity.OpenDate.ToUnixTimestamp(),
                    CloseDate = entity.CloseDate !=null ? entity.CloseDate.Value.ToUnixTimestamp() : (long?) null
                };
        }

        /// <summary>
        /// Map from an array of account entities to an array
        ///  of read-only account view models
        /// </summary>
        /// <param name="entities">array of account entities</param>
        /// <returns>array of read-only account view models</returns>
        internal static AccountRm[] From(ICollection<Account> entities)
        {
            return entities.Select(From).ToArray();
        }
    }
}