using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.NewParam.Rules.Expressions
{
    public abstract class ParamExpression<T, K> where T : new()
    {
        #region ctor

        protected ParamExpression()
        {
        }

        protected ParamExpression(WebParamState state)
        {
            this.State = state;
        }

        protected ParamExpression(string message)
        {
            this.Message = message;
        }

        protected ParamExpression(WebParamState state, string message)
        {
            this.State = state;
            this.Message = message;
        }

        #endregion

        public virtual WebParamState State
        {
            get;
            protected set;
        }

        public string Message
        {
            get;
            protected set;
        }

        internal abstract void Execute(BaseWebParamItem<T, K> value);

        public static ParamExpression<T, K> And(ParamExpression<T, K> left, ParamExpression<T, K> right)
        {
            if (left == null) return right;
            if (right == null) return left; 
            return new ParamBinaryExpression<T, K>(left, right);
        }

        public static ParamExpression<T, K> operator +(ParamExpression<T, K> left, ParamExpression<T, K> right)
        {
            return ParamExpression<T, K>.And(left, right);
        }
    }
}
