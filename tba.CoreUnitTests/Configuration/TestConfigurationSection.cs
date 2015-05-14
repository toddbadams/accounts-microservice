using System.Configuration;

namespace tba.CoreUnitTests.Configuration
{
    public class TestConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("MyBooleanSetting", IsRequired = true)]
        public bool MyBooleanSetting
        {
            get { return (bool)this["MyBooleanSetting"]; }
        }
    }
}