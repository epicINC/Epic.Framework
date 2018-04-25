using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.NewParam.Rules.Expressions
{
    internal class ActionExpression<T, K> : ParamExpression<T, K> where T : new()
    {
        internal ActionExpression(Action<K> action)
        {
            this.Action = action;
        }

        internal Action<K> Action
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
