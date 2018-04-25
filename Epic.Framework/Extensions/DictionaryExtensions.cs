using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 从 Dictionary<K, V> 中移除指定的键的值
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public static void Remove<K, V>(this Dictionary<K, V> source, IEnumerable<K> dest)
        {
            dest.ForEach(e => source.Remove(e));
        }


        public static void Intersect<K, V>(this Dictionary<K, V> source, IEnumerable<K> dest)
        {
            source.Remove(source.Keys.Except(dest).ToList());
        }
    }
}
