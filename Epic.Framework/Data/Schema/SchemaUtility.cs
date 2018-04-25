using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Epic.Data.Schema
{
    internal static class SchemaUtility
    {
        public static T GetCustomAttributes<T>(this ICustomAttributeProvider type) where T : class
        {
  
            var attrs = type.GetCustomAttributes(typeof(T), true);
            if (attrs.Length == 0) return default(T);
            return attrs[0] as T;
        }


    }
}
