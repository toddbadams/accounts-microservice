using Newtonsoft.Json;

namespace tba.Core.Utilities
{
    public static class Serialization
    {
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj,
                new JsonSerializerSettings() { Formatting = Formatting.None });
        }

        public static object Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
