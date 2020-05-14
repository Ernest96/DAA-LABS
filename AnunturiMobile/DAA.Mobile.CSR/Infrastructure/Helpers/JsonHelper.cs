using Newtonsoft.Json;

namespace Anunturi.Mobile.Infrastructure.Helpers
{
    public static class JsonHelper
    {
        public static string SerializeToJson(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return json;
        }

        public static T DeserializeFromJson<T>(string json)
        {
            var result = JsonConvert.DeserializeObject<T>(json);
            return result; 
        }
    }
}
