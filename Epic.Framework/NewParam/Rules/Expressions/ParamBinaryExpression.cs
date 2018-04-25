using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.NewParam.Rules.Expressions
{
    internal class ParamBinaryExpression<T, K> : ParamExpression<T, K> where T : new()
    {
        internal ParamBinaryExpression(ParamExpression<T, K> left, ParamExpression<T, K> right)
        {
            this.Left = left;
            this.Right = right;
        }

        internal ParamExpression<T, K> Left
        {
            get;
            private set;
        }

        internal ParamExpression<T, K> Right
        {
            get;
            private set;
        }

        internal override void Execute(BaseWebParamItem<T, K> value)
        {
            this.Left.Execute(value);
            this.Right.Execute(value);
        }
        
    }
}
