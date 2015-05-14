using tba.Core.Configuration;

namespace tba.CoreUnitTests.Configuration
{
    public class TestConfigurationProvider : ConfigurationProviderBase<TestConfigurationSection>
    {
        public TestConfigurationProvider(string filename)
            : base(filename)
        {
        }
    }
}