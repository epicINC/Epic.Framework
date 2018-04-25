using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Epic.Extensions;
using Epic.Components;

namespace Epic.Utility
{

    internal class EnumOperation<T, K> : IEnumOperation<T, K> where T : struct, IEnumConstraint
    {

        internal EnumOperation()
        {
            var paramType1 = typeof(T);
            var paramType2 = typeof(K);
            var underlyingType = paramType1.GetEnumUnderlyingType();

            var param1 = Expression.Parameter(paramType1, "left");
            var param2 = Expression.Parameter(paramType2, "right");
            var convertedParam1 = Expression.Convert(param1, underlyingType);
            var convertedParam2 = (paramType2 == underlyingType).IIF <Expression>(param2, Expression.Convert(param2, underlyingType));


            this.Not = Expression.Lambda<Func<K, T>>(Expression.Convert(Expression.Not(convertedParam2), paramType1), param2).Compile();
            this.IsEmpty = Expression.Lambda<Func<T, bool>>(Expression.Equal(convertedParam1, Expression.Constant(Activator.CreateInstance(underlyingType))), param1).Compile();

            this.Equality = Expression.Lambda<Func<T, K, bool>>(Expression.Equal(convertedParam1, convertedParam2), param1, param2).Compile();
            this.Or = Expression.Lambda<Func<T, K, T>>(Expression.Convert(Expression.Or(convertedParam1, convertedParam2), paramType1), param1, param2).Compile();
            this.And = Expression.Lambda<Func<T, K, T>>(Expression.Convert(Expression.And(convertedParam1, convertedParam2), paramType1), param1, param2).Compile();
            
            
            this.Set = this.Xor = Expression.Lambda<Func<T, K, T>>(Expression.Convert(Expression.ExclusiveOr(convertedParam1, convertedParam2), paramType1), param1, param2).Compile();
            this.Add = this.Or;
            this.Remove = Expression.Lambda<Func<T, K, T>>(Expression.Convert(Expression.And(convertedParam1, Expression.Not(convertedParam2)), paramType1), param1, param2).Compile();
            this.HasValue = Expression.Lambda<Func<T, K, bool>>(Expression.Equal(Expression.And(convertedParam1, convertedParam2), convertedParam2), param1, param2).Compile();

        }

        public Func<T, K, bool> HasValue
        {
            get;
            private set;
        }

        public Func<T, K, bool> Equality
        {
            get;
            private set;
        }

        public Func<T, K, T> Or
        {
            get;
            private set;
        }

        public Func<T, K, T> And
        {
            get;
            private set;
        }

        public Func<T, K, T> Xor
        {
            get;
            private set;
        }

        public Func<K, T> Not
        {
            get;
            private set;
        }

        public Func<T, K, T> Set
        {
            get;
            private set;
        }

        public Func<T, K, T> Add
        {
            get;
            private set;
        }

        public Func<T, K, T> Remove
        {
            get;
            private set;
        }

        public Func<T, bool> IsEmpty
        {
            get;
            private set;
        }
    }
}
