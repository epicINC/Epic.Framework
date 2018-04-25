using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Data.Objects
{
    internal static class TypeSystem
    {

        
        




        internal static Type GetElementType(Type type)
        {
            Type result = FindIEnumerable(type);
            if (result == null)
                return type;
            return type.GetGenericArguments()[0];
        }


        static Type FindIEnumerable(Type seqType)
        {
            if (((seqType != null) && (seqType != typeof(string))) && (seqType != typeof(byte[])))
            {
                if (seqType.IsArray)
                {
                    return typeof(IEnumerable<>).MakeGenericType(new Type[] { seqType.GetElementType() });
                }
                if (seqType.IsGenericType)
                {
                    foreach (Type type in seqType.GetGenericArguments())
                    {
                        Type type2 = typeof(IEnumerable<>).MakeGenericType(new Type[] { type });
                        if (type2.IsAssignableFrom(seqType))
                        {
                            return type2;
                        }
                    }
                }
                Type[] interfaces = seqType.GetInterfaces();
                if ((interfaces != null) && (interfaces.Length > 0))
                {
                    foreach (Type type3 in interfaces)
                    {
                        Type type4 = FindIEnumerable(type3);
                        if (type4 != null)
                        {
                            return type4;
                        }
                    }
                }
                if ((seqType.BaseType != null) && (seqType.BaseType != typeof(object)))
                {
                    return FindIEnumerable(seqType.BaseType);
                }
            }
            return null;
        }
    }
}
