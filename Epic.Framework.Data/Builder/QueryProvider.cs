using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;

namespace Epic.Data.Builder
{
    /// <summary>
    /// http://blogs.msdn.com/b/mattwar/archive/2008/11/18/linq-links.aspx
    /// </summary>
    public abstract class QueryProvider : IQueryProvider
    {
        public IQueryable<T> CreateQuery<T>(Expression expression)
        {
            return new Query<T>(this, expression);
        }

        public Query<T> CreateQuery<T>()
        {
            return new Query<T>(this);
        }


        public IQueryable CreateQuery(Expression expression)
        {
            var elementType = TypeSystem.GetElementType(expression.Type);

            return (IQueryable)typeof(Query<>).MakeGenericType(new Type[] { elementType }).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).Single<ConstructorInfo>().Invoke(new object[] { this, expression });
        }

        internal T ExecuteInternal<T>(Query<T> query)
        {
            return default(T);
        }

        T IQueryProvider.Execute<T>(Expression expression)
        {
            return this.ExecuteInternal(new Query<T>(this, expression));
        }


        object IQueryProvider.Execute(Expression expression)
        {
            return this.CreateQuery(expression);
        }

        public abstract string GetQueryText(Expression expression);

        public abstract object Execute(Expression expression);

    }
}
