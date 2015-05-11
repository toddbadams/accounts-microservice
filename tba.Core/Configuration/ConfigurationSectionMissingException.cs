using System;

namespace tba.Core.Configuration
{
    public class ConfigurationSectionMissingException : Exception
    {
        public ConfigurationSectionMissingException()
        {

        }

        public ConfigurationSectionMissingException(string message)
            : base(message)
        {

        }

        public ConfigurationSectionMissingException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}