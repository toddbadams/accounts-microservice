using System.Configuration;

namespace tba.Users.Configuration
{
    public class UsersConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// URL of the users microservice
        /// </summary>
        [ConfigurationProperty("ServiceUrl", IsRequired = true)]
        public string ServiceUrl
        {
            get { return (string)this["ServiceUrl"]; }
            set { this["ServiceUrl"] = value; }
        }
    }
}
