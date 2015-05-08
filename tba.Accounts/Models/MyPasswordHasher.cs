using System.Linq;

namespace tba.Accounts.Models
{
    public class MyPasswordHasher
    {
        public string CreateHash(string password)
        {
            // FOR DEMO ONLY! Use a standard method or 
            // crypto library to do this for real:
            char[] chars = password.ToArray();
            char[] hash = chars.Reverse().ToArray();
            return new string(hash);
        }
    }
}