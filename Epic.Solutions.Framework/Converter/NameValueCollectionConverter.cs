using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Converter
{
    public static class NameValueCollectionConverter
    {
        public static Dictionary<string, string> AsDictionary(this NameValueCollection value)
        {
            var result = new Dictionary<string, string>();
            for (int i = 0; i < value.Count; i++)
            {
                result.Add(value.Keys[i], value[i]);
            }
            return result;
        }

        public static string AsQueryString(this NameValueCollection value)
        {
            return DictionaryConverter.AsQueryString(AsDictionary(value));
        }
    }
}
