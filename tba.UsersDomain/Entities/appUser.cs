using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace tba.Users.Entities
{
    // You can add profile data for the user by adding more properties to your 
    // appUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class AppUser : IdentityUser, IIdentity
    {
        // Id is string in dB.

        // From IdentityUser
        // Email:string
        // EmailConfirmed:boolean
        // PasswordHash:string
        // SecurityStamp:string
        // PhoneNumber:string
        // PhoneNumberConfirmed:boolean
        // TwoFactorEnabled:boolean
        // LockoutEndDateUtc:Nullable<DateTime>
        // LockoutEnabled:boolean
        // AccessFailedCount:int
        // Roles:ICollection<TRole>
        // Claims:ICollection<TClaim>
        // Logins:ICollection<TLogin>

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }

        public int TimeZone { get; set; }
        [Required]
        [MaxLength(22)]
        public string TenantId { get; set; }

        [MaxLength(256)]
        public string AvatarUrl { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            // Note the authenticationType must match the one defined in 
            // CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string Name { get; private set; }
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
    }
}