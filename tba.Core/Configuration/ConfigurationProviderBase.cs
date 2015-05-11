using System;
using System.Configuration;

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
        /// <param name="sectionName"></param>
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
            // Create the mapping.
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = fqFilename };

            // Open the configuration.
            _config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }

        /// <summary>
        /// Read the configuration file.
        /// </summary>
        /// <returns>A configuration section</returns>
        public T Read()
        {
            var m = string.Format("Unable to read configuration section {0}", typeof(T).Name);
            try
            {
                var section = GetSection() as T;
                if (section == null)
                    throw new ConfigurationSectionMissingException(m);
                return section;
            }
            catch (Exception ex)
            {
                throw new ConfigurationSectionMissingException(m, ex);
            }
        }

        /// <summary>
        /// Get the section from the default configuration file or from the custom one.
        /// </summary>
        /// <returns></returns>
        private object GetSection()
        {
            var section = _config != null
                ? _config.GetSection(_sectionName)
                : ConfigurationManager.GetSection(_sectionName);
            return section;
        }
    }
}