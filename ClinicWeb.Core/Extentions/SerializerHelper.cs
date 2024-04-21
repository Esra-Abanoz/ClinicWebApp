using Newtonsoft.Json;

namespace ClinicWeb.Core.Extentions
{
    public static class SerializerHelper
    {
        public static string Serialize<T>(T objectToCache)
        {
            return JsonConvert.SerializeObject(objectToCache);
        }
        private static List<T> DeserializeList<T>(List<string> jsonStrings)
        {
            return
                jsonStrings.Select(
                    data =>
                    {

                        return JsonConvert.DeserializeObject<T>(data);
                    }
                ).ToList();
        }

        public static T Deserialize<T>(string redisObject)
        {
            return JsonConvert.DeserializeObject<T>(redisObject);
        }
        public static object Deserialize<T>(Stream stream, Type type)
        {
            var serializer = new JsonSerializer();
            using (var reader = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(reader))
            {
                return serializer.Deserialize(jsonTextReader);
            }
        }
        public static object DeserializeObject(string redisObject, Type type)
        {
            return JsonConvert.DeserializeObject(redisObject, type);
        }
    }
}
