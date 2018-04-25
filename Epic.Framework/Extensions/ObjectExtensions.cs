using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Extensions
{
    /// <summary>
    /// 引用自 System.Data.Entity.ModelConfiguration.Utilities
    /// </summary>
    internal static class ObjectExtensions
    {
        public static IEnumerable<T> AsEnumerable<T>(this T value) where T : class
        {
            if (value == null)
                return Enumerable.Empty<T>();
            return new T[]{value};
        }
    }
}
