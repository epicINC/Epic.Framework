using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Epic.Data.V2.Pagings;
using Epic.Components;

namespace Epic.Data.V2
{

    public static class ObjectQueryExtension
    {
        public static int Count<T>(this IObjectQuery<T> value)
        {
            value.Builder.Count = true;
            return value.Provider.Count(value);
        }

        public static T Single<T>(this IObjectQuery<T> value)
        {
            return value.Take(1).SingleOrDefault();
        }

 

        public static IObjectQuery<T> Where<T>(this IObjectQuery<T> value, Expression<Func<T, bool>> expression)
        {
            return value.And(expression);
        }

        public static IObjectQuery<T> And<T>(this IObjectQuery<T> value, Expression<Func<T, bool>> expression)
        {
            value.Builder.And(expression);
            return value;
        }

        public static IObjectQuery<T> Or<T>(this IObjectQuery<T> value, Expression<Func<T, bool>> expression)
        {
            value.Builder.Or(expression);
            return value;
        }

        public static IObjectQuery<T> OrderBy<T>(this IObjectQuery<T> value, string key, SortDirection direction)
        {
            value.Builder.OrderBy[key] = direction;
            return value;
        }


        public static IObjectQuery<T> Take<T>(this IObjectQuery<T> value, int limit)
        {
            value.Builder.Limit = limit;
            return value;
        }

        public static IObjectQuery<T> Skip<T>(this IObjectQuery<T> value, int skip)
        {
            value.Builder.Skip = skip;
            return value;
        }


        public static PageList<T> Paged<T>(this IObjectQuery<T> value, int request, int size)
        {
            value.Builder.Func = QueryFuncType.Paging;

            value.Take(size).Skip((request - 1) * size);
            value.Param.AbsolutePage = request;
            value.Param.PageSize = size;


            var result = new PageList<T>();
            result.AddRange(value.ToList());
            result.Paging = value.Param;
            result.Paging.RecordCount = (int)value.ParameterData["@RecordCount"].Value;

            return result;
        }


        #region Text


        public static IObjectQuery<T> Where<T>(this IObjectQuery<T> value, string query)
        {

            value.Builder.And(query);
            return value;
        }


        public static IObjectQuery<T> And<T>(this IObjectQuery<T> value, string query)
        {

            value.Builder.And(query);
            return value;
        }


        public static IObjectQuery<T> Or<T>(this IObjectQuery<T> value, string query)
        {
            value.Builder.Or(query);

            return value;
        }


        #endregion

    }

}
