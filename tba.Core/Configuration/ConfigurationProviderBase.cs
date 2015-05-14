using System;
using System.Configuration;
using tba.Core.Exceptions;

namespace tba.Core.Configuration
{
    public abstract class ConfigurationProviderBase<T> : IConfigurationProvider<T> where T : ConfigurationSection
    {
        /// <summary>
        /// Name of the section to read from the app.config.
        /// </summary>
        private readonly string _sectionName;

        /// <summary>
        /// Config object used to read a custom configuration file.
        /// If not set the default file will be used.
        /// </summary>
        private System.Configuration.Configuration _config;

        /// <summary>
        /// Initialize the provider.
        /// </summary>
        /// <param name="sectionName">the configuration section name</param>
        protected ConfigurationProviderBase(string sectionName)
        {
            _sectionName = sectionName;
        }

        /// <summary>
        /// Set a custom configuration file.
        /// </summary>
        /// <param name="fqFilename">fully qualified filename of the config file</param>
        public void SetConfigurationFile(string fqFilename)
        {
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = fqFilename };
            _config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }

        /// <summary>
        /// Read the configuration file.
        /// </summary>
        /// <returns>A configuration section</returns>
        public T Read()
        {
            var m = string.Format("Unable to read configuration section {0}", _sectionName);
            try
            {
                var section = (_config != null
                    // custom config file
                    ? _config.GetSection(_sectionName)
                    // default config file
                    : ConfigurationManager.GetSection(_sectionName)) as T;

                if (section == null)
                    throw new ConfigurationSectionMissingException(m);

                return section;
            }
            catch (Exception ex)
            {
                throw new ConfigurationSectionMissingException(m, ex);
            }
        }
    }
}