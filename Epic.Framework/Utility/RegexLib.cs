using System;
using System.Text.RegularExpressions;

namespace Epic.Utility
{
    public static class RegexLib
    {
        /// <summary>
        /// 标准用户的正则表达式 英文名 英文开头 数字 2-11 字符, 中文名 中文开头 英文 数字 1-11 字符
        /// </summary>
        public const string Name = "^[a-z][A-Za-z0-9]{2,11}$|^[\u4e00-\u9fa5][\u4e00-\u9fa5A-Za-z0-9]{1,11}$";

        /// <summary>
        /// 标准密码的正则表示式 6-12 字符
        /// </summary>
        public const string Password = @"^\S{6,12}$";

        /// <summary>
        /// 标准 Email 的正则表达式
        /// </summary>
        public const string Email = @"^[-a-zA-Z0-9_\.]+\@([0-9A-Za-z][0-9A-Za-z-]+\.)+[A-Za-z]{2,5}$";

        /// <summary>
        /// 标准 Oicq 的正则表示式
        /// 5 - 10 位数字
        /// </summary>
        public const string Oicq = @"^[0-9]{5,10}$";

        /// <summary>
        /// 手机号码正则表达式
        /// 中国移动 139|138|137|136|135|134|159|150|151|158|157|188|187|152|182|147
        /// 中国联通 130|131|132|155|156|185|186|145
        /// 中国电信 133|153|180|181|189
        /// </summary>
        public const string ChineseMobile = @"^(139|138|137|136|135|134|159|150|151|158|157|188|187|152|182|147|130|131|132|155|156|185|186|145|133|153|180|181|189)\d{8}$";

        /// <summary>
        /// 电话正则表达式(包括国际电话)
        /// </summary>
        public const string GlobalPhone = @"^(?:[\+|(]?\d{1,4}[\)]?[s|-]+?)?(?:[\(]?\d{1,4}[\)|\s|-]?)?\d{3,11}(?:[s|-]+?\d{1,4})?$";

        /// <summary>
        /// 使用 , 分割的 ID 字符串
        /// </summary>
        public const string IDStrings = @"([\d+][,]?)+";


        internal static Regex InvalidXmlChar = new Regex("[\x00-\x08|\x0b-\x0c|\x0e-\x1f]+", RegexOptions.Compiled);
    }
}
