using Newtonsoft.Json;

namespace Selivura.DemoClicker.Persistence
{
    public class JsonSerializer : ISerializer
    {
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}
