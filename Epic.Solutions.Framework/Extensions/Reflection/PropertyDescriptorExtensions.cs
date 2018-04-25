using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Utility;

namespace Epic.Extensions
{
    public static class PropertyDescriptorExtensions
    {
        public static T GetCustomAttribute<T>(this PropertyDescriptor value) where T : Attribute
        {
            return ReflectionUtility.GetCustomAttribute<T>(value);
        }

        public static Attribute GetCustomAttribute(this PropertyDescriptor value, Type attributeType)
        {
            return ReflectionUtility.GetCustomAttribute(value, attributeType);
        }
    }
}
