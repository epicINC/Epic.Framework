using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Utility
{
    public static class JsonUtility
    {
        public static string Serialize(object value)
        {
            try
            {
                return JsonConvert.SerializeObject(value);
            }
            catch (JsonSerializationException)
            {

                return null;
            }
        }

        public static T Deserialize<T>(string value)
        {
            if (String.IsNullOrWhiteSpace(value)) return default(T);

            try
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception)
            {
                return default(T);
            }
        }


        static void CheckOrCreateDirectory(string path)
        {
            var dir = new DirectoryInfo(path);
            if (dir.Exists) return;
            dir.Create();
        }


        public static void SerializeToFile<T>(T value, string path)
        {
            CheckOrCreateDirectory(Path.GetDirectoryName(path));

            using (var sw = new StreamWriter(path, false))
            {
                (new JsonSerializer()).Serialize(sw, value);
            }

        }

        public static T DeserializeFromFile<T>(string path)
        {
            if (!File.Exists(path)) return default(T);

            try
            {
                using (var sr = new StreamReader(path, true))
                {
                    return (T)(new JsonSerializer()).Deserialize(sr, typeof(T));
                }
            }
            catch (JsonSerializationException)
            {
                return default(T);
            }
        }
    }
}
