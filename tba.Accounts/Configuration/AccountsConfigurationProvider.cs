using tba.Core.Configuration;

namespace tba.Accounts.Configuration
{
    public class AccountsConfigurationProvider : ConfigurationProviderBase<AccountsConfigurationSection>
    {
        public AccountsConfigurationProvider()
            : base("AppConfiguration/Accounts")
        {
        }
    }
}