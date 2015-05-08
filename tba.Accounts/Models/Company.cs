using System.ComponentModel.DataAnnotations;
using System.Text;

namespace tba.Accounts.Models
{
    //public class Role
    //{
    //    public Role()
    //    {
    //        Id = Guid.NewGuid().ToString();
    //    }

    //    [Key]
    //    public string Id { get; set; }
    //    public string Name { get; set; }
    //    public List<UserRole> Users { get; set; }
    //}


    //public class UserRole
    //{
    //    public string UserId { get; set; }
    //    public User User { get; set; }

    //    public string RoleId { get; set; }
    //    public Role Role { get; set; }
    //}


    //public class UserManager
    //{
    //    UserStore _userStore;
    //    public UserManager(UserStore userStore)
    //    {
    //        _userStore = userStore;
    //    }


    //    public async Task CreateAsync(User user, string password)
    //    {
    //        if(await _userStore.UserExists(user))
    //        {
    //            throw new Exception("A user with that Email address already exists");
    //        }
    //        var hasher = new PasswordHasher();
    //        user.PasswordHash = hasher.CreateHash(password);
    //        await _userStore.AddUserAsync(user);
    //    }


    //    public async Task AddClaim(string UserId, Claim claim)
    //    {
    //        _userStore.AddClaim()
    //    }
    //}

    public class Company
    {
        // Add Key Attribute:
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
