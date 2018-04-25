using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections;
using System.ComponentModel;
using Epic.Components;


namespace Epic.Data.Objects
{

    public abstract class ObjectQuery : IOrderedQueryable
    {
        ObjectQueryState state;

        internal ObjectQuery(ObjectQueryState state)
        {
            this.state = state;
        }

        internal abstract ObjectResult ExecuteInternal();
        internal abstract IEnumerator GetEnumeratorInternal();
        internal abstract Expression GetExpression();


        public Type ElementType
        {
            get { return this.state.ElementType; }
        }

        public Expression Expression
        {
            get { return this.GetExpression(); }
        }

        public IQueryProvider Provider
        {
            get { return this.Context.QueryProvider; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumeratorInternal();
        }

        public ObjectContext Context
        {
            get { return this.state.ObjectContext; }
        }

        internal ObjectQueryState QueryState
        {
            get { return this.state; }
        }

    }

    public class ObjectQuery<T> : ObjectQuery, IOrderedQueryable<T>
    {
        internal ObjectQuery(ObjectQueryState queryState) : base(queryState)
        {
        }

        internal ObjectQuery(string commandText, ObjectContext context) : base(new ObjectQueryState(typeof(T), commandText, context))
        {
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.GetResults().GetEnumerator();
        }

        internal override ObjectResult ExecuteInternal()
        {
            return this.GetResults();
        }

        internal override IEnumerator GetEnumeratorInternal()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        internal override Expression GetExpression()
        {
            Expression expression;
            if (!base.QueryState.TryGetExpression(out expression))
                expression = Expression.Constant(this);
            return expression;
        }

        ObjectResult<T> GetResults()
        {
            return base.QueryState.GetExecutionPlan().Execute<T>();
        }

        public ObjectQuery<T> Where(string predicate)
        {
            DataUtil.CheckArgumentNull(predicate, "predicate");

            base.QueryState.Builder.Where = predicate;


            return this;

        }


    }
}
