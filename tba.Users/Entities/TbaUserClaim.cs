using System;
using System.ComponentModel.DataAnnotations;

namespace tba.Users.Models
{
    public class TbaUserClaim
    {
        public TbaUserClaim()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}