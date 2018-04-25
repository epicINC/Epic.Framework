using System;

namespace Epic.Web.Expressions
{
    internal class ValidatorExpression<T> : ParamExpression<T>
    {
        internal ValidatorExpression(Predicate<T> action)
        {
            this.action = action;
        }

        Predicate<T> action;

        internal override void Build(HttpParam<T> param)
        {
                param.RaiseEvent(action, HttpParamStateType.ValidateFail);

        }
    }
}
