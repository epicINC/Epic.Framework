using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Epic.Web;

namespace Epic.Data.V2
{
    public static class ObjectQueryParamExtension
    {
        public static IObjectQuery<T> And<T, P>(this IObjectQuery<T> value,  HttpParam<P> httpParam, Expression<Func<T, bool>> expression)
        {
            if (!httpParam.IsValid()) return value;
            value.Param.Add(httpParam);
            return value.And(expression);
        }

        public static IObjectQuery<T> Or<T, P>(this IObjectQuery<T> value, HttpParam<P> httpParam, Expression<Func<T, bool>> expression)
        {
            if (!httpParam.IsValid()) return value;
            value.Param.Add(httpParam);
            return value.Or(expression);
        }


        public static IObjectQuery<T> And<T, P>(this IObjectQuery<T> value, HttpParam<P> httpParam, string query)
        {
            if (!httpParam.IsValid()) return value;
            value.Param.Add(httpParam);
            return value.And(query);
        }

        public static IObjectQuery<T> Or<T, P>(this IObjectQuery<T> value, HttpParam<P> httpParam, string query)
        {
            if (!httpParam.IsValid()) return value;
            value.Param.Add(httpParam);
            return value.Or(query);
        }
    }
}
