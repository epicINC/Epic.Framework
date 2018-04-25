using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace Epic.Data.Mongo.Samus.FluentAPI
{
    public static class QueryObjectMethod
    {
        public static PropertyDescriptor GetDescriptor<T>(string methodName)
        {
            return TypeDescriptor.GetProperties(typeof(T)).Find(methodName, false);
        }

        

        public static PropertyCache<T, K> Property<T, K>(string propertyName)
        {
            return Property<T, K>(typeof(T), propertyName);
        }


        public static PropertyCache<T, K> Property<T, K>(Type type, string propertyName)
        {
            var item = type.GetProperty(propertyName);
            if (item == null) return null;
            return new PropertyCache<T, K>()
            {
                Get = (Func<T, K>)Delegate.CreateDelegate(typeof(Func<T, K>), item.GetGetMethod()),
                Set = (Action<T, K>)Delegate.CreateDelegate(typeof(Action<T, K>), item.GetSetMethod())
            };
        }

    }


    public class PropertyCache<T, K>
    {

        public Func<T, K> Get
        {
            get;
            set;
        }

        public Action<T, K> Set
        {
            get;
            set;
        }

    }
}
