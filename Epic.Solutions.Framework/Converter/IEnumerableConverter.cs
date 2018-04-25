using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Extensions;

namespace Epic.Converter
{
    public static class IEnumerableConverter
    {

        public static IEnumerable<K> Convert<T, K>(IEnumerable<T> collection, TryParse<T, K> parser, Action<K> action = null)
        {
            var result = new List<K>();
            K local;
            foreach (var item in collection)
            {
                if (parser(item, out local))
                    result.AddOrIgnore(local, action);
            }
            return result;
        }


        public static IEnumerable<K> Convert<T, K>(IEnumerable<T> collection, Converter<T, K> converter, Action<K> action = null)
        {
            var result = new List<K>();
            collection.ForEach(e => result.AddOrIgnore(converter(e), action));
            return result;
        }
    }
}
