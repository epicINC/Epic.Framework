using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Linq.Expressions;
using Epic.FluentAPI;

namespace Epic.Emit
{
    public static class FastPropertyAccessor<T>
    {

        static FastPropertyAccessor()
        {
            Accessor = new FastPropertyAccessor(typeof(T));
        }

        static FastPropertyAccessor Accessor
        {
            get;
            set;
        }

        public static object Get(T instance, string propertyName)
        {
            return Accessor.Get(instance, propertyName);
        }

        public static void Set(T instance, string propertyName, object value)
        {
            Accessor.Set(instance, propertyName, value);
        }

        public static K Get<K>(T instance, Expression<Func<T, K>> expr)
        {
            return (K)Get(instance, SimpleAccess.Property(expr).Name);
        }

        public static void Set<K>(T instance, Expression<Func<T, K>> expr, K value)
        {
            Set(instance, SimpleAccess.Property(expr).Name, value);
        }

    }

}
