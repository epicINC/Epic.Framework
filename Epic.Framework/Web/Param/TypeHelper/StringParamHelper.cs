using System;
using System.Text.RegularExpressions;

namespace Epic.Web
{
    public static class StringParamHelper
    {
        #region Parse 转换方法

        public static HttpParam<string> Parse(this HttpParam<string> param)
        {
            //if (param.original == null)
            //    param.state = HttpParamStateType.ParseError;
            //if (param.original == String.Empty)
            //    param.state = HttpParamStateType.Empty;
            //param.value = param.original;
            //return param;
            return param.Parse(
                delegate(string input, out string output)
                {
                    output = input;
                    if (String.IsNullOrWhiteSpace(input))
                        return false;
                    return true;
                });
        }

        #endregion


        #region 文本校验 String Validator

        /// <summary>
        /// 常用用户名校验
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static HttpParam<string> Name(this HttpParam<string> param)
        {
            return param.Validator(Utility.ValidatorLibrary.NameValidator);
        }

        /// <summary>
        /// 常用密码校验
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static HttpParam<string> IsPassword(this HttpParam<string> param)
        {
            return param.Validator(Utility.ValidatorLibrary.PasswordValidator);
        }

        /// <summary>
        /// 常用 Email 校验
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static HttpParam<string> IsEmail(this HttpParam<string> param)
        {
            return param.Validator(Utility.ValidatorLibrary.EmailValidator);
        }

        /// <summary>
        /// 国内手机号码校验
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static HttpParam<string> ChineseMobile(this HttpParam<string> param)
        {
            return param.Validator(Utility.ValidatorLibrary.ChineseMobileValidator);
        }

        /// <summary>
        /// 全球电话号码校验
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static HttpParam<string> GlobalPhone(this HttpParam<string> param)
        {
            return param.Validator(Utility.ValidatorLibrary.GlobalPhoneValidator);
        }

        /// <summary>
        /// 国内身份证号码校验(同时支持 15位和 18位 身份证)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static HttpParam<string> Identitycard(this HttpParam<string> param)
        {
            return param.Validator(Utility.ValidatorLibrary.IdentitycardValidator);
        }

        /// <summary>
        /// Oicq 号码校验
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static HttpParam<string> Oicq(this HttpParam<string> param)
        {
            return param.Validator(Utility.ValidatorLibrary.OicqValidator);
        }

        #endregion


        #region 动作 Action

        static Regex xmlCharReplace = new Regex("[\x00-\x08|\x0b-\x0c|\x0e-\x1f]+", RegexOptions.Compiled);

        /// <summary>
        /// 转换为 Xml 安全文本
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static HttpParam<string> SafeXml(this HttpParam<string> param)
        {
            return param.Do(delegate(string item)
            {
                item = xmlCharReplace.Replace(item, String.Empty);
            });
        }

        /// <summary>
        /// 转换为 查询 安全文本
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static HttpParam<string> SafeQuery(this HttpParam<string> param)
        {
            return param.Do(delegate(string item)
            {
                return item.Replace("'", String.Empty);
            });
        }

        #endregion

        #region 范围 Range

        public static HttpParam<string> Range(this HttpParam<string> param, int min, int max)
        {
            param.Range(delegate(string item)
            {
                if (String.IsNullOrEmpty(item)) return false;
                if (item.Length < min) return false;
                if (item.Length > max) return false;
                return true;
            });
            return param;
        }

        /// <summary>
        /// 最小长度
        /// </summary>
        /// <param name="param"></param>
        /// <param name="min">大于等于该值 为有效</param>
        /// <returns></returns>
        public static HttpParam<string> Min(this HttpParam<string> param, int min)
        {
            param.Range(delegate(string item)
            {
                if (String.IsNullOrEmpty(item)) return false;
                if (item.Length < min) return false;
                return true;
            });
            return param;
        }

        /// <summary>
        /// 最大长度
        /// </summary>
        /// <param name="param"></param>
        /// <param name="max">小于等于该值 为有效</param>
        /// <returns></returns>
        public static HttpParam<string> Max(this HttpParam<string> param, int max)
        {
            param.Range(delegate(string item)
            {
                if (String.IsNullOrEmpty(item)) return false;
                if (item.Length > max) return false;
                return true;
            });
            return param;
        }

        #endregion


        #region Encode

        public static string MD5(this HttpParam<string> param)
        {
            if (!param.IsValid()) return null;
            return Security.Utility.MD5(param.value);
        }

        #endregion


        #region 正则表达式 Regex

        /// <summary>
        /// 是否匹配
        /// </summary>
        /// <param name="param"></param>
        /// <param name="pattern">正则表达式文本</param>
        /// <returns></returns>
        public static HttpParam<string> IsMatch(this HttpParam<string> param, string pattern)
        {
            param.Validator(delegate(string item)
            {
                return item == pattern;
            });
            return param;
        }

        /// <summary>
        /// 是否匹配
        /// </summary>
        /// <param name="param"></param>
        /// <param name="regex">正则对象</param>
        /// <returns></returns>
        public static HttpParam<string> IsMatch(this HttpParam<string> param, Regex regex)
        { 
            param.Validator(delegate(string item)
            {
                return regex.IsMatch(item);
            });
            return param;
        }

        /// <summary>
        /// 替换
        /// </summary>
        /// <param name="param"></param>
        /// <param name="pattern">查找</param>
        /// <param name="replcement">替换</param>
        /// <returns></returns>
        public static HttpParam<string> Replace(this HttpParam<string> param, string find, string replcement)
        {
            return param.Do(delegate(string item)
            {
                return item.Replace(find, replcement);
            });
        }

        /// <summary>
        /// 替换
        /// </summary>
        /// <param name="param"></param>
        /// <param name="regex">查找</param>
        /// <param name="replcement">替换</param>
        /// <returns></returns>
        public static HttpParam<string> Replace(this HttpParam<string> param, Regex regex, string replcement)
        {
            return param.Do(delegate(string item)
            {
                return regex.Replace(item, replcement);
            });
        }


        #endregion


        #region Length

        public static int Length(this HttpParam<string> param)
        {
            return param.value.Length;
        }

        #endregion
    }
}
