using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tba.Accounts.Models
{
    public class MyUser
    {
        public MyUser()
        {
            Id = Guid.NewGuid().ToString();
            Claims = new List<MyUserClaim>();
        }

        [Key]
        public string Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<MyUserClaim> Claims { get; set; }
    }
}