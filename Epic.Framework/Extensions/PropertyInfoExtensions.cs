using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Epic.Extensions
{
    /// <summary>
    /// 引用自 System.Data.Entity.ModelConfiguration.Utilities.PropertyInfoExtensions 略做修改
    /// </summary>
    internal static class PropertyInfoExtensions
    {
        public static bool IsSameAs(this PropertyInfo value, PropertyInfo other)
        {
            return value.DeclaringType == other.DeclaringType && value.Name == other.Name;
        }

    }
}
