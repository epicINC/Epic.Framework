using System;
using System.Text.RegularExpressions;
using Epic.Extensions;

namespace Epic.Utility
{
    public static class ValidatorLibrary
    {

        /// <summary>
        /// IP 地址 正确与否判断
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IPValidator(string input)
        {
            if (input.Length > 15) return false;
            var s = input.Replace(".", String.Empty);
            if ((input.Length - s.Length) != 4) return false;

            uint result;
            return UInt32.TryParse(s, out result);
        }


        /// <summary>
        /// 检查输入字符串是否为空, 是否符合正则
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>验证结果</returns>
        static bool CheckValue(string input, string pattern)
        {
            if (String.IsNullOrEmpty(input))
                return false;
            return Regex.IsMatch(input, pattern);
        }


        /// <summary>
        /// 名字验证
        /// </summary>
        /// <param name="input">需要验证的 名字</param>
        /// <returns>验证结果</returns>
        public static bool NameValidator(string input)
        {
            return CheckValue(input, RegexLib.Name);
        }

        /// <summary>
        /// 密码验证
        /// </summary>
        /// <param name="input">需要验证的 密码</param>
        /// <returns>验证结果</returns>
        public static bool PasswordValidator(string input)
        {
            return CheckValue(input, RegexLib.Password);
        }

        /// <summary>
        /// Oicq 验证
        /// </summary>
        /// <param name="input">需要验证的 Oicq</param>
        /// <returns>验证结果</returns>
        public static bool OicqValidator(string input)
        {
            return CheckValue(input, RegexLib.Oicq);
        }

        public static bool OicqValidator(long input)
        {
            return input > 10000 && input < 10000000000;
        }

        /// <summary>
        /// Email 验证
        /// </summary>
        /// <param name="input">需要验证的 Email</param>
        /// <returns>验证结果</returns>
        public static bool EmailValidator(string input)
        {
            return CheckValue(input, RegexLib.Email);
        }

        /// <summary>
        /// 手机号码验证
        /// </summary>
        /// <param name="input">需要验证的 手机号码</param>
        /// <returns>验证结果</returns>
        public static bool ChineseMobileValidator(string input)
        {
            return CheckValue(input, RegexLib.ChineseMobile);
        }

        /// <summary>
        /// 电话号码验证
        /// </summary>
        /// <param name="input">需要验证的 电话号码</param>
        /// <returns>验证结果</returns>
        public static bool GlobalPhoneValidator(string input)
        {
            return CheckValue(input, RegexLib.GlobalPhone);
        }

        /// <summary>
        /// 判断字符串是否是 , 分割的 id 字串.
        /// </summary>
        /// <param name="input">需要检查的文本</param>
        /// <returns>检查结果</returns>
        public static bool IDStringValidator(string input)
        {
            return CheckValue(input.Replace(" ", ""), RegexLib.IDStrings);
        }


        /// <summary>
        /// 身份证检查(同时支持 15 位和 18 位)
        /// </summary>
        /// <param name="input">身份证号码</param>
        /// <returns>返回校验结果</returns>
        public static bool IdentitycardValidator(string input)
        {
            if (String.IsNullOrEmpty(input)) return false;
            if (input.Length == 18) return CheckIdentitycard18(input);
            if (input.Length == 15) return CheckIdentitycard15(input);
            return false;
        }


        static bool CheckIdentitycard18(string id)
        {
            long n;
            if (!long.TryParse(id.Remove(17), out n) || n < Math.Pow(10, 16) || !long.TryParse(id.Replace('x', '0').Replace('X', '0'), out n)) return false;//数字验证

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2)) == -1) return false;//省份验证

            string birth = id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time;
            if (!DateTime.TryParse(birth, out time)) return false;//生日验证

            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());

            int y;
            Math.DivRem(sum, 11, out y);
            return (arrVarifyCode[y] == id.Substring(17, 1).ToLower());//校验码验证 //符合GB11643-1999标准
        }

        static bool CheckIdentitycard15(string id)
        {
            long n;
            if (!long.TryParse(id, out n) || n < Math.Pow(10, 14)) return false;//数字验证

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2)) == -1) return false;//省份验证

            string birth = id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time;
            return DateTime.TryParse(birth, out time); //生日验证
        }

    }
}
