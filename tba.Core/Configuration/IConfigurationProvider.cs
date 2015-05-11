using System.Configuration;

namespace tba.Core.Configuration
{
    public interface IConfigurationProvider<T>
        where T : ConfigurationSection 
    {
        void SetConfigurationFile(string fqFilename);
        T Read();
    }
}
