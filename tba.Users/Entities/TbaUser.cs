using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tba.Core.Entities;
using tba.Core.Persistence.Extensions;
using tba.Users.Models;

namespace tba.Users.Entities
{
    public class TbaUser : Entity, IEmail
    {
        /// <summary>
        /// User email, must be unique within the data store
        /// </summary>
        [MaxLength(64)]
        public string Email { get; set; }

        /// <summary>
        /// Hashed user password
        /// </summary>
        [MaxLength(256)]
        public string PasswordHash { get; set; }

        /// <summary>
        /// A collection of user claims
        /// </summary>
        public virtual ICollection<TbaUserClaim> Claims { get; set; }

        public static TbaUser Create(string email, string passwordHash)
        {
            var u = new TbaUser
            {
                Email = email,
                PasswordHash = passwordHash,
                Claims = new List<TbaUserClaim>()
            };
            return u;
        }
    }
}