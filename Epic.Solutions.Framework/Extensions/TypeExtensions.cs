using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Extensions
{
    public static class TypeExtensions
    {
        public static T GetCustomAttributes<T>(this Type value, bool inherit = true, int index = 0) where T : class
        {
            var result = value.GetCustomAttributes(typeof(T), inherit);
            if (result == null || result.Length == 0) return null;
            return result[index] as T;
        }

    }
}
