using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Epic.Serialization
{
    /// <summary>
    /// 预计 实现 IFormatter
    /// </summary>
    public static class JsonFormatter
    {
        /// <summary>
        /// 把对象格式化成 Json 文本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Serialize<T>(T value)
        {
            if (value == null) return null;

            using (var ms = new MemoryStream())
            {
                var ser = new DataContractJsonSerializer(typeof(T));
                ser.WriteObject(ms, value);
                ms.Position = 0;
                using (var sr = new StreamReader(ms, System.Text.Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 把文本格式化成 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string value)
        {
            if (String.IsNullOrWhiteSpace(value)) return default(T);

            using (var ms = new MemoryStream())
            {
                using (var sr = new StreamWriter(ms))
                {
                    sr.Write(value);
                    sr.Flush();
                    ms.Position = 0;
                    return (T)(new DataContractJsonSerializer(typeof(T))).ReadObject(ms);

                }
            }
        }
    }
}
