using tba.Users.Entities;

namespace tba.Users.Models
{
    // Models returned by MeController actions.
    public class UserRm
    {
        // From IdentityUser
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }

        // from appUser
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TimeZone { get; set; }

        public static UserRm Create(AppUser user)
        {
            return new UserRm
            {
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                LockoutEnabled = user.LockoutEnabled,
                FirstName = user.FirstName,
                LastName = user.LastName,
                TimeZone = user.TimeZone,

            };
        }

        public AppUser ToApplicationUser()
        {
            return new AppUser
            {
                Email = Email,
                EmailConfirmed = EmailConfirmed,
                PhoneNumber = PhoneNumber,
                PhoneNumberConfirmed = PhoneNumberConfirmed,
                TwoFactorEnabled = TwoFactorEnabled,
                LockoutEnabled = LockoutEnabled,
                FirstName = FirstName,
                LastName = LastName,
                TimeZone = TimeZone
            };
        }
    }
}