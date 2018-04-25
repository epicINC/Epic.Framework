using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Epic.Extensions.Expressions
{
    /// <summary>
    /// 表达式扩展对象
    /// 作用: 方便使用
    /// 20120620
    /// </summary>
    public static class ExpressionExtensions
    {

        public static ParameterExpression Parameter<T>(string name = null)
        {
            return Expression.Parameter(typeof(T), name);
        }

        public static MethodCallExpression Call<T>(Func<T> method)
        {
            if (method == null)
                throw new ArgumentNullException("method");
            return Expression.Call(null, method.Method);
        }


        #region 运算符

        /// <summary>
        /// 加法, 自动创建两 ParameterExpression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static BinaryExpression Add<T>(string left, string right)
        {
            return Add<T, T>(left, right);
        }

        /// <summary>
        /// 加法, 自动创建两 ParameterExpression
        /// </summary>
        /// <returns></returns>
        public static BinaryExpression Add<T, K>(string left, string right)
        {
            return Expression.Add(Parameter<T>(left), Parameter<K>(right));
        }

        /// <summary>
        /// 加法, value 自动创建 ConstantExpression
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static BinaryExpression Add(Expression exp, object value)
        {
            return Expression.Add(exp, Expression.Constant(value));
        }

        #region 乘法

        /// <summary>
        /// 乘法, 自动创建两 ParameterExpression
        /// </summary>
        /// <returns></returns>
        public static BinaryExpression Multiply<T>(string left, string right)
        {
            return Multiply<T, T>(left, right);
        }

        /// <summary>
        /// 乘法, 自动创建两 ParameterExpression
        /// </summary>
        /// <returns></returns>
        public static BinaryExpression Multiply<T, K>(string left, string right)
        {
            return Expression.Multiply(Parameter<T>(left), Parameter<K>(right));
        }

        #endregion

        #endregion
    }
}
