using System;

namespace Epic.Web.Expressions
{
    public enum ParamExpressionType
    {
        None,
        Parse,
        Validator,
        Range,
        Do
    }



    internal abstract class ParamExpression<T>
    {
        internal abstract void Build(HttpParam<T> param);



        public static ParamExpression<T> And(ParamExpression<T> left, ParamExpression<T> right)
        {
            if (right == null)
                throw Error.ArgumentNull("right");
            if (left == null)
                return right;


            return new ParamBinaryExpression<T>(left, right);
        }



        public static ParamExpression<T> operator +(ParamExpression<T> left, ParamExpression<T> right)
        {
            return ParamExpression<T>.And(left, right);
            // where State & 2 = 2
        }
    }
}
