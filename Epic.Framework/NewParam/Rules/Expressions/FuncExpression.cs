using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.NewParam.Rules.Expressions
{
    internal class FuncExpression<T, K> : ParamExpression<T, K> where T : new()
    {
        internal FuncExpression(Func<K, K> action)
        {
            this.Action = action;
        }

        internal Func<K, K> Action
        {
            get;
            private set;
        }

        internal override void Execute(BaseWebParamItem<T, K> value)
        {
            value.RaiseEvent(this);
        }
    }
}
