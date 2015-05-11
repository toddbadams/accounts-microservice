using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tba.Core.Configuration;

namespace tba.CoreUnitTests.Configuration
{
    [TestClass]
    public class ConfigurationProviderBaseTest
    {
        private IConfigurationProvider<TestConfigurationSection> _provider;

        [TestInitialize]
        public void TestInitialize()
        {
            var appBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            _provider = new TestConfigurationProvider("AppConfiguration/Test");
            _provider.SetConfigurationFile(BuildFqn(appBase, "TestSection.xml"));
        }


        [TestMethod]
        public void Should_Create_TestConfigurationSection()
        {
            // Arrange

            // Act
            var result = _provider.Read();

            // Assert
            Assert.IsTrue(result.MyBooleanSetting);
        }


        private static string BuildFqn(string appBase, string filename)
        {
            var fqn = appBase + "\\..\\..\\Configuration\\TestData\\" + filename;
            return fqn;
        }
    }

    public class TestConfigurationProvider : ConfigurationProviderBase<TestConfigurationSection>
    {
        public TestConfigurationProvider(string filename)
            : base(filename)
        {
        }
    }

    public class TestConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("MyBooleanSetting", IsRequired = true)]
        public bool MyBooleanSetting
        {
            get { return (bool)this["MyBooleanSetting"]; }
        }
    }
}
