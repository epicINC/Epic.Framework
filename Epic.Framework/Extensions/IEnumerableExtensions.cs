using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Extensions
{
    public static class IEnumerableExtensions
    {

        public static K[] ToArray<T, K>(this IEnumerable<T> value, Func<T, K> func)
        {
            return value.ToList(func).ToArray();
        }

        public static List<K> ToList<T, K>(this IEnumerable<T> value, Func<T, K> func)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            if (func == null)
                throw new ArgumentNullException("action");

            var result = new List<K>();
            foreach (var item in value)
                result.Add(func(item));
            return result;
        }
       

        /// <summary>
        /// 对指定序列的每个元素执行指定操作。
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="value">序列</param>
        /// <param name="action">执行方法</param>
        public static void ForEach<T>(this IEnumerable<T> value, Action<T> action)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            if (action == null)
                throw new ArgumentNullException("action");

            foreach (var item in value)
                action(item);
        }

        /// <summary>
        /// 转换对象序列
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="value">序列</param>
        /// <param name="convert">转换方法</param>
        /// <returns>返回转换结果 true 转换全部成功, false 转换有不成功</returns>
        public static bool Convert<T>(this IEnumerable<T> value, Func<T, bool> convert)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            if (convert == null)
                throw new ArgumentNullException("convert");

            bool state = true;

            foreach (var item in value)
            {
                if (!convert(item))
                {
                    state = false;
                    continue;
                }
            }

            return state;

   
        }

        public static bool Contains<T>(this IEnumerable<T> source, Predicate<T> action)
        {
            foreach (var item in source)
            {
                if (action(item))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 取两个集合交集
        /// </summary>
        /// <typeparam name="T">源集合类型</typeparam>
        /// <typeparam name="K">目标集合类型</typeparam>
        /// <param name="source">源集合</param>
        /// <param name="second">目标集合</param>
        /// <param name="comparer">对比方法</param>
        /// <returns>交集</returns>
        public static IEnumerable<T> Intersect<T, K>(this IEnumerable<T> source, IEnumerable<K> second, Func<T, K, bool> comparer)
        {
            var result = new List<T>();
            foreach (var item in second)
            {
                Intersect<T>(result, source.FirstOrDefault(e => comparer(e, item)));
            }
            return result;
        }

        internal static void Intersect<T>(List<T> collection, T value)
        {
            if (value == null) return;
            collection.Add(value);
        }

    }
}
