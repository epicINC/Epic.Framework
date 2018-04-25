using System;

namespace Epic.Web.Expressions
{
    internal class FuncExpression<T> : ParamExpression<T>
    {
        internal FuncExpression(Func<T, T> action)
        {
            this.action = action;
        }

        Func<T, T> action;

        internal override void Build(HttpParam<T> param)
        {
                param.RaiseEvent(action);

        }
    }
}
