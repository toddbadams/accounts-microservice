using System.Configuration;

namespace tba.Core.Configuration
{
    public interface IConfigurationProvider<out T>
        where T : ConfigurationSection 
    {
        void SetConfigurationFile(string fqFilename);
        T Read();
    }
}
