using tba.Core.Configuration;

namespace tba.Users.Configuration
{
    public class UsersConfigurationProvider : ConfigurationProviderBase<UsersConfigurationSection>
    {
        public UsersConfigurationProvider()
            : base("AppConfiguration/Users")
        {
        }
    }
}