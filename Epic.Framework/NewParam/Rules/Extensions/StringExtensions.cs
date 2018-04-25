using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Epic.NewParam
{
    public static class RuleForExpressionStringExtensions
    {
        #region Range

        public static RuleForExpression<T, string> Min<T>(this RuleForExpression<T, string> value, int min, string message) where T : new()
        {
            return value.Valid(e => !String.IsNullOrEmpty(e) && e.Length > min, WebParamState.ValidateFail, message);
        }

        public static RuleForExpression<T, string> Max<T>(this RuleForExpression<T, string> value, int max, string message) where T : new()
        {
            return value.Valid(e => !String.IsNullOrEmpty(e) && e.Length < max, WebParamState.ValidateFail, message);
        }

        public static RuleForExpression<T, string> Range<T>(this RuleForExpression<T, string> value, int min, int max, string message) where T : new()
        {
            return value.Valid(e => !String.IsNullOrEmpty(e) && e.Length > min && e.Length < max, WebParamState.ValidateFail, message);
        }

        #endregion


        #region Encode 

        public static RuleForExpression<T, string> MD5<T>(this RuleForExpression<T, string> value) where T : new()
        {
            return value.Change(Security.Utility.MD5);
        }


        #endregion

        /// <summary>
        /// 表达式验证
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="value">验证对象</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="options">正则选项</param>
        /// <param name="message">提示消息</param>
        /// <returns></returns>
        public static RuleForExpression<T, string> Regex<T>(this RuleForExpression<T, string> value, string pattern, RegexOptions options = RegexOptions.None, string message = null) where T : new()
        {
            return value.Valid(e => !String.IsNullOrEmpty(e) && System.Text.RegularExpressions.Regex.IsMatch(e, pattern, options), WebParamState.ValidateFail, message);
        }

        /// <summary>
        /// Email 验证
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static RuleForExpression<T, string> Email<T>(this RuleForExpression<T, string> value, string message = null) where T : new()
        {
            return value.Valid(e => !String.IsNullOrEmpty(e) && Utility.ValidatorLibrary.EmailValidator(e), WebParamState.ValidateFail, message);
        }

        /// <summary>
        /// 国内手机验证
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static RuleForExpression<T, string> Mobile<T>(this RuleForExpression<T, string> value, string message = null) where T : new()
        {
            return value.Valid(e => !String.IsNullOrEmpty(e) && Utility.ValidatorLibrary.ChineseMobileValidator(e), WebParamState.ValidateFail, message);
        }

        /// <summary>
        /// 身份证验证
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static RuleForExpression<T, string> Identitycard<T>(this RuleForExpression<T, string> value, string message = null) where T : new()
        {
            return value.Valid(e => !String.IsNullOrEmpty(e) && Utility.ValidatorLibrary.IdentitycardValidator(e), WebParamState.ValidateFail, message);
        }

        /// <summary>
        /// Oicq 号码验证
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static RuleForExpression<T, string> Oicq<T>(this RuleForExpression<T, string> value, string message = null) where T : new()
        {
            return value.Valid(e => !String.IsNullOrEmpty(e) && Utility.ValidatorLibrary.OicqValidator(e), WebParamState.ValidateFail, message);
        }

    }
}
