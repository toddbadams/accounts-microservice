﻿using System.Configuration;

namespace tba.Accounts.Configuration
{
    public class AccountsConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// URL of the accounts microservice
        /// </summary>
        [ConfigurationProperty("ServiceUrl", IsRequired = true)]
        public string ServiceUrl
        {
            get { return (string)this["ServiceUrl"]; }
            set { this["ServiceUrl"] = value; }
        }
    }
}
