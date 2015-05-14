using System;
using NUnit.Framework;
using tba.Core.Configuration;

namespace tba.CoreUnitTests.Configuration
{
    [TestFixture]
    public class ConfigurationProviderBaseTest
    {
        private IConfigurationProvider<TestConfigurationSection> _provider;
        private string _appBase;
        private readonly Func<string, string, string> _buildFqn =
            ((s, s1) => s + "\\..\\..\\Configuration\\TestData\\" + s1);

        [SetUp]
        public void TestInitialize()
        {
            _appBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            _provider = new TestConfigurationProvider("AppConfiguration/Test");
        }


        [Test]
        [ExpectedException("tba.Core.Configuration.ConfigurationSectionMissingException")]
        public void Should_Throw_When_TestConfigurationSection_Missing()
        {
            // Arrange
            _provider.SetConfigurationFile(_buildFqn(_appBase, "TestSectionMissing.xml"));

            // Act
            _provider.Read();

            // Assert
        }

        [Test]
        public void Should_Create_TestConfigurationSection()
        {
            // Arrange
            _provider.SetConfigurationFile(_buildFqn(_appBase, "TestSection.xml"));

            // Act
            var result = _provider.Read();

            // Assert
            Assert.IsTrue(result.MyBooleanSetting);
        }
    }
}
