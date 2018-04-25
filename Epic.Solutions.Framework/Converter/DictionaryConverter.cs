using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Extensions;

namespace Epic.Converter
{
    public static class DictionaryConverter
    {
        public static string AsQueryString<T, K>(this Dictionary<T, K> dictionary, bool removeEmptyEntry = false)
        {
            if (removeEmptyEntry)
                return AsQueryString(dictionary, RemoveEmptyEntry);
            return String.Join("&", dictionary.Select(e => e.Key + "=" + e.Value));
        }


        public static string AsQueryString<T, K>(this Dictionary<T, K> dictionary, Func<KeyValuePair<T, K>, bool> filter)
        {
            if (filter != null)
                return dictionary.Where(filter).StringJoin("&", e => e.Key +"="+ e.Value);


            return String.Join("&", dictionary.Select(e => e.Key + "=" + e.Value));
        }

        internal static bool RemoveEmptyEntry<T, K>(KeyValuePair<T, K> e)
        {
            return e.Value != null && !e.Value.Equals(String.Empty);
        }

    }
}
