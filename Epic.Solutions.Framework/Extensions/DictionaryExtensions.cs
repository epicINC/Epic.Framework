using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Extensions
{
    public static class DictionaryExtensions
    {
        public static K Read<T, K>(this Dictionary<T, K> value, T key)
        {
            K result;
            value.TryGetValue(key, out result);
            return result;
        }

        public static string AsQueryString<T, K>(this Dictionary<T, K> value, bool removeEmptyEntry = false)
        {
            return Epic.Converter.DictionaryConverter.AsQueryString(value, removeEmptyEntry);
        }

        public static IEnumerable<KeyValuePair<T, K>> RemoveEmptyEntry<T, K>(this Dictionary<T, K> value)
        {
            return value.Where(Epic.Converter.DictionaryConverter.RemoveEmptyEntry);
        }
    }
}
