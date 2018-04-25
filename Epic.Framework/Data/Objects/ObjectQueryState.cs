using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using Epic.Components;

namespace Epic.Data.Objects
{
    public class ObjectQueryState
    {
        readonly Type elementType;
        readonly ObjectContext context;
        readonly Expression expression;

        readonly string queryText;

        internal ObjectQueryState(Type elementType, ObjectContext context, Expression expression)
        {
            this.elementType = elementType;
            this.context = context;
            this.expression = expression;
        }

        internal ObjectQueryState(Type elementType, string commandText, ObjectContext context)
            : this(elementType, context, null)
        {
            this.queryText = commandText;
        }


        Expressions.DbCommandBuilder builder;
        internal Expressions.DbCommandBuilder Builder
        {
            get
            {
                if (this.builder == null)
                    this.builder = new Expressions.DbCommandBuilder();
                return this.builder;
            }
        }


        internal Type ElementType
        {
            get { return this.elementType; }
        }

        internal ObjectContext ObjectContext
        {
            get { return this.context; }
        }

        internal bool TryGetExpression(out Expression expression)
        {
            expression = this.expression;
            return this.expression !=  null;
        }


        internal ObjectQueryExecutionPlan GetExecutionPlan()
        {

            //Expressions.SqlExpressionVisitor.Build(this.expression);
            var builder = Expressions.MSSqlExpressionVisitor.Build(this.Builder, this.expression);
            return new ObjectQueryExecutionPlan(this.context, builder.CommandText, builder.Parameters);
        }




        public static ObjectQuery<T> CreateObjectQuery<T>(ObjectQueryState queryState)
        {
            return new ObjectQuery<T>(queryState);
        }


        internal ObjectQuery CreateQuery()
        {
            return (ObjectQuery)typeof(ObjectQueryState).GetMethod("CreateObjectQuery", BindingFlags.Public | BindingFlags.Static).MakeGenericMethod(new Type[] { this.elementType }).Invoke(null, new object[] { this });
        }
    }
}
