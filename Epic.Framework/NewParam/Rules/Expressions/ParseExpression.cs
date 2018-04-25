using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.NewParam.Rules.Expressions
{
    internal sealed class ParseExpression<T, K> : ParamExpression<T, K> where T : new()
    {

        internal ParseExpression(ParseAction<string, K[]> action, string message) : base(message)
        {
            this.ArrayAction = action;
        }

        internal ParseExpression(ParseAction<string, K> action, string message) : base(message)
        {
            this.Action = action;
        }

        public override WebParamState State
        {
            get { return WebParamState.ParseError; }
            protected set {}
        }

        internal ParseAction<string, K[]> ArrayAction
        {
            get;
            private set;
        }

        internal ParseAction<string, K> Action
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
