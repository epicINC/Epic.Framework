using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Epic.Components;

namespace Epic.Data.Objects
{
    internal sealed class ObjectQueryProvider : IQueryProvider
    {

        internal static T ExecuteSingle<T>(IEnumerable<T> query, Expression node)
        {
            //return query.FirstOrDefault();
            return GetElementFunction<T>(node)(query);
        }

        static Func<IEnumerable<T>, T> GetElementFunction<T>(Expression node)
        {
            if (node.NodeType == ExpressionType.Lambda)
                node = ((LambdaExpression)node).Body;
            if (node.NodeType == ExpressionType.Call)
            {
                var method = (MethodCallExpression)node;
                switch (method.Method.Name)
                {
                    case "First" :
                    case "FirstPredicate":
                        return e => e.First<T>();
                    case "FirstOrDefault" :
                    case "FirstOrDefaultPredicate":
                        return e => e.FirstOrDefault<T>();
                    case "SingleOrDefault":
                    case "SingleOrDefaultPredicate" :
                        return e => e.SingleOrDefault<T>();
                    default:
                        break;
                }
            }
            return e => e.Single<T>();
        }

        readonly ObjectContext context;

        internal ObjectQueryProvider(ObjectContext context)
        {
            this.context = context;
        }

        ObjectQuery<T> CreateQuery<T>(Expression expression)
        {
            return new ObjectQuery<T>(new ObjectQueryState(typeof(T), this.context, expression));
        }

        ObjectQuery CreateQuery(Expression expression, Type type)
        {
            ObjectQueryState state = new ObjectQueryState(type, this.context, expression);
            return state.CreateQuery();
        }

        IQueryable<T> IQueryProvider.CreateQuery<T>(Expression expression)
        {
            return this.CreateQuery<T>(expression);
        }

        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            Type elementType = TypeSystem.GetElementType(expression.Type);
            return this.CreateQuery(expression, elementType);
        }

        T IQueryProvider.Execute<T>(Expression expression)
        {

            return ExecuteSingle<T>(this.CreateQuery<T>(expression), expression);
        }

        object IQueryProvider.Execute(Expression expression)
        {
            return ExecuteSingle<object>(this.CreateQuery(expression, expression.Type).Cast<object>(), expression);
        }
    }
}
