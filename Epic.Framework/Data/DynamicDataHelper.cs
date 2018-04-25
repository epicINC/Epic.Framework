using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Data.SqlClient;

namespace Epic.Data
{
    public static class DynamicDataHelper
    {
        static Dictionary<Type, Delegate> cache = new Dictionary<Type,Delegate>();


        public static Func<IDataRecord, T> DynamicCreateEntity<T>()
        {
            var type = typeof(T);

            
            if (!cache.ContainsKey(type))
            {
                var result = DynamicCreateEntity<T>(type);
                cache.Add(type, result);
                return result;
            }

            return (Func<IDataRecord, T>)cache[type];
        }



        static Func<IDataRecord, T> DynamicCreateEntity<T>(Type type)
        {    

            ParameterExpression r = Expression.Parameter(typeof(IDataRecord), "r");

            var method = typeof(UtilityHelper).GetMethod("Field");

            List<MemberBinding> bindings = new List<MemberBinding>();
            foreach (PropertyInfo property in (type.GetProperties()))
            {
                var propertyValue = Expression.Call(method.MakeGenericMethod(property.PropertyType), r, Expression.Constant(property.Name));
                var binding = Expression.Bind(property, propertyValue);
                bindings.Add(binding);
            }
            var initializer = Expression.MemberInit(Expression.New(type), bindings);
            Expression<Func<IDataRecord, T>> lambda = Expression.Lambda<Func<IDataRecord, T>>(initializer, r);
            return lambda.Compile();
        } 
    }

    public static class UtilityHelper
    {
        public static T Field<T>(this IDataReader dr, string key)
        {
            return (T)dr[key];
        }
    }
}
