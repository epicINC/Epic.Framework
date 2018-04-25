using System;

namespace Epic.Web.Expressions
{
    internal class RangeExpression<T> : ParamExpression<T>
    {
        internal RangeExpression(Predicate<T> action)
        {
            this.action = action;
        }

        Predicate<T> action;

        internal override void Build(HttpParam<T> param)
        {
                param.RaiseEvent(action, HttpParamStateType.OutOfRange);

        }
    }
}
