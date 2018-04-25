using System;

namespace Epic.Web.Expressions
{

    internal class ParseExpression<T> : ParamExpression<T>
    {
        internal ParseExpression(ParseAction<string, T> action)
        {
            this.action = action;
        }

        ParseAction<string, T> action;

        internal override void Build(HttpParam<T> param)
        {
            param.RaiseEvent(action, HttpParamStateType.ParseError);

        }
    }
}
