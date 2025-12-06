using System.IO;
using Newtonsoft.Json;

namespace LifeSim.Core
{
    public static class SaveSystem
    {
        public static void Save(string path, object obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public static T Load<T>(string path)
        {
            if (!File.Exists(path)) return default(T);
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
