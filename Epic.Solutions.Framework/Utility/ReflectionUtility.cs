using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Utility
{
    public static class ReflectionUtility
    {

        public static T GetCustomAttribute<T>(PropertyDescriptor value) where T : Attribute
        {
            return GetCustomAttribute(value, typeof(T)) as T;
        }

        public static Attribute GetCustomAttribute(PropertyDescriptor value, Type attributeType)
        {
            for (int i = 0; i < value.Attributes.Count; i++)
            {
                if (value.Attributes[i].GetType() == attributeType)
                    return value.Attributes[i];
            }
            return null;
        }
    }
}
