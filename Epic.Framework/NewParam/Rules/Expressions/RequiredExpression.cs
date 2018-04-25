using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.NewParam.Rules.Expressions
{
    internal sealed class RequiredExpression<T, K> : ParamExpression<T, K> where T : new()
    {
        internal RequiredExpression(Predicate<string> action, string message = null) : base(message)
        {
            this.Action = action;
        }

        public override WebParamState State
        {
            get { return WebParamState.Empty; }
            protected set { }
        }

        internal Predicate<string> Action
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
