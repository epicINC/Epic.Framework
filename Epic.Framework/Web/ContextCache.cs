using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Epic.Web
{
    public class ContextCache
    {
        const string prefix = "Epic.ContentCache.";

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">缓存的对象</typeparam>
        /// <param name="key">缓存的键</param>
        /// <returns>返回缓存中的对象</returns>
        public static T Get<T>(string key)//  where T : class;
        {
            return Get<T>(HttpContext.Current, prefix + key);
        }

        static T Get<T>(HttpContext context, string key)
        {
            if (context == null) goto Fail;
            var local = context.Items[key];
            if (local == null) goto Fail;
            return (T)local;

            Fail:
            return default(T);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存的键</param>
        /// <param name="value">缓存的值</param>
        public static void Set<T>(string key, T value)
        {
            Set(System.Web.HttpContext.Current, prefix + key, value);
        }

        static void Set<T>(HttpContext context, string key, T value)
        {
            if (context == null) return;
            context.Items[key] = value;
        }
    }
}
