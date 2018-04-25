using System;

namespace Epic.Web.Expressions
{
    internal class ParamBinaryExpression<T> : ParamExpression<T>
    {
        internal ParamBinaryExpression(ParamExpression<T> left, ParamExpression<T> right)
        {
            this.left = left;
            this.right = right;
        }

        ParamExpression<T> left;
        ParamExpression<T> right;


        internal override void Build(HttpParam<T> param)
        {
            left.Build(param);
            right.Build(param);
        }
    }
}
