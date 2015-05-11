using System.Configuration;

namespace tba.Accounts.Configuration
{
    public class AccountsConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("ServiceUrl", IsRequired = true)]
        public string ServiceUrl
        {
            get { return (string)this["ServiceUrl"]; }
        }
    }
}
