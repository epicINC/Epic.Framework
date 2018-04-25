using System;

namespace Epic.Web.Expressions
{
    internal class ActionExpression<T> : ParamExpression<T>
    {
        internal ActionExpression(Action<T> action)
        {
            this.action = action;
        }

        Action<T> action;

        internal override void Build(HttpParam<T> param)
        {
                param.RaiseEvent(action);
        }
    }
}
