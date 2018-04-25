using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Epic.NewParam.Rules.Expressions;

namespace Epic.NewParam
{
    public static class RuleForExpressionExtensions
    {

        #region 来源

        /// <summary>
        /// 设置原始值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="value"></param>
        /// <param name="original">原始值</param>
        /// <returns></returns>
        public static RuleForExpression<T, K> Original<T, K>(this RuleForExpression<T, K> value, string original) where T : new()
        {
            value.Item.Original = original;
            return value;
        }

        /// <summary>
        /// 设置来源集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="value"></param>
        /// <param name="collection">集合</param>
        /// <param name="alias">Key 名称(如不填则取属性名)</param>
        /// <returns></returns>
        public static RuleForExpression<T, K> Collection<T, K>(this RuleForExpression<T, K> value, NameValueCollection collection, string alias = null) where T : new()
        {
            value.Item.Alias = alias;
            return value.Original<T, K>(collection[String.IsNullOrWhiteSpace(alias) ? value.Item.Name : alias]);
        }

        /// <summary>
        /// QueryString 集合取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="value"></param>
        /// <param name="alias">Key 名称(如不填则取属性名)</param>
        /// <returns></returns>
        public static RuleForExpression<T, K> QueryString<T, K>(this RuleForExpression<T, K> value, string alias = null) where T : new()
        {
            return value.Collection<T, K>(value.Item.Parent.Context.Request.QueryString, alias);
        }

        /// <summary>
        /// Form 集合取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="value"></param>
        /// <param name="alias">Key 名称(如不填则取属性名)</param>
        /// <returns></returns>
        public static RuleForExpression<T, K> Form<T, K>(this RuleForExpression<T, K> value, string alias = null) where T : new()
        {
            return value.Collection<T, K>(value.Item.Parent.Context.Request.Form, alias);
        }

        /// <summary>
        /// Params 集合取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="value"></param>
        /// <param name="alias">Key 名称(如不填则取属性名)</param>
        /// <returns></returns>
        public static RuleForExpression<T, K> Params<T, K>(this RuleForExpression<T, K> value, string alias = null) where T : new()
        {
            return value.Collection<T, K>(value.Item.Parent.Context.Request.Params, alias);
        }


        #endregion





        /// <summary>
        /// 设置转换器(如不填写则使用默认转换器)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="value"></param>
        /// <param name="action">转换器</param>
        /// <param name="message">错误提示信息</param>
        /// <returns></returns>
        public static RuleForExpression<T, K> Parse<T, K>(this RuleForExpression<T, K> value, ParseAction<string, K> action, string message = null) where T : new()
        {
            value.Item.ParseExpression += new ParseExpression<T, K>(action, message);
            return value;
        }

        public static RuleForExpression<T, K[]> Parse<T, K>(this RuleForExpression<T, K[]> value, ParseAction<string, K[]> action, string message = null) where T : new()
        {
            value.Item.ParseExpression += new ParseExpression<T, K[]>(action, message);
            return value;
        }

        /// <summary>
        /// 设置必须
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="value"></param>
        /// <param name="message">错误提示信息</param>
        /// <returns></returns>
        public static RuleForExpression<T, K> Required<T, K>(this RuleForExpression<T, K> value, string message = null) where T : new()
        {
            return value.Required(String.IsNullOrEmpty, message);
        }

        /// <summary>
        /// 设置必须
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="value"></param>
        /// <param name="action">必填验证方法</param>
        /// <param name="message">错误提示信息</param>
        /// <returns></returns>
        public static RuleForExpression<T, K> Required<T, K>(this RuleForExpression<T, K> value, Predicate<string> action, string message = null) where T : new()
        {
            value.Item.RequiredExpression += new RequiredExpression<T, K>(action, message);
            return value;
        }

        /// <summary>
        /// 设置验证器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="value"></param>
        /// <param name="action">验证方法</param>
        /// <param name="state">验证失败后状态</param>
        /// <param name="message">错误提示信息</param>
        /// <returns></returns>
        public static RuleForExpression<T, K> Valid<T, K>(this RuleForExpression<T, K> value, Predicate<K> action, WebParamState state = WebParamState.ValidateFail, string message = null) where T : new()
        {
            value.Item.Expression += new ValidExpression<T, K>(action, state, message);
            return value;
        }

        /// <summary>
        /// 设置转换器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="value"></param>
        /// <param name="action">转换方法</param>
        /// <returns></returns>
        public static RuleForExpression<T, K> Change<T, K>(this RuleForExpression<T, K> value, Func<K, K> action) where T : new()
        {
            value.Item.Expression += new FuncExpression<T, K>(action);
            return value;
        }

        /// <summary>
        /// 设置操作器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="value"></param>
        /// <param name="action">操作方法</param>
        /// <returns></returns>
        public static RuleForExpression<T, K> Do<T, K>(this RuleForExpression<T, K> value, Action<K> action) where T : new()
        {
            value.Item.Expression += new ActionExpression<T, K>(action);    
            return value;
        }



    }

}
