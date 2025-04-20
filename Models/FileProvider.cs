using Newtonsoft.Json;
using System.IO;

namespace BeautifulWeather.Models
{
    public static class FileProvider
    {
        public static void Save(object data, string fileName)
        {
            string serializedData = JsonConvert.SerializeObject(data);

            if (!File.Exists(fileName)) File.Create(fileName).Close();

            File.WriteAllText(fileName, serializedData);
        }

        public static T Load<T>(string fileName)
        {
            if (!File.Exists(fileName)) return default;

            var jsonString = File.ReadAllText(fileName);
            if (string.IsNullOrEmpty(jsonString)) return default;

            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
