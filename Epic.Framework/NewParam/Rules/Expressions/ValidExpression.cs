using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.NewParam.Rules.Expressions
{
    internal sealed class ValidExpression<T, K> : ParamExpression<T, K> where T : new()
    {
        internal ValidExpression(Predicate<K> action, WebParamState state = WebParamState.ValidateFail, string message = null) : base(state, message)
        {
            this.Action = action;
        }

        internal Predicate<K> Action
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
