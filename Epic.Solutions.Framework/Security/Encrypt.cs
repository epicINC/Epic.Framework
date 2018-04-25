using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Epic.Components;
using Epic.Extensions;

namespace Epic.Security
{
    /// <summary>
    /// 加密类
    /// </summary>
    public static class Encryption
    {
        public static string Encrypt(AccountPasswordEncryptType encryptType, string format, string password, string salt)
        {
            switch (encryptType)
            {
                case AccountPasswordEncryptType.MD516Salt:
                    break;
                case AccountPasswordEncryptType.MD532Salt:
                    return MD5(EncryptFormat(format, password, salt));
                case AccountPasswordEncryptType.MD516:
                    break;
                case AccountPasswordEncryptType.MD532:
                    return MD5(EncryptFormat(format, password));
                case AccountPasswordEncryptType.PlanText:
                    break;
                case AccountPasswordEncryptType.SHA1Salt:
                    return SHA1(EncryptFormat(format, password, salt));
                case AccountPasswordEncryptType.SHA256Salt:
                    break;
                case AccountPasswordEncryptType.SHA512Salt:
                    break;
                case AccountPasswordEncryptType.SHA1:
                    return SHA1(EncryptFormat(format, password));
                case AccountPasswordEncryptType.SHA256:
                    break;
                case AccountPasswordEncryptType.SHA512:
                    break;
                case AccountPasswordEncryptType.Salt:
                    break;
                default:
                    break;
            }
            return password;
        }

        static string EncryptFormat(string format, string password, string salt)
        {
            if (!String.IsNullOrEmpty(format))
                return String.Format(format, password, salt);
            return password + salt;
        }

        static string EncryptFormat(string format, string password)
        {
            if (!String.IsNullOrEmpty(format))
                return String.Format(format, password);
            return password;
        }


        /// <summary>
        /// MD5 算法
        /// </summary>
        /// <param name="value">需要加密的字串</param>
        /// <returns>加密结果</returns>
        public static string MD5(this string value)
        {
            return ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(Encoding.UTF8.GetBytes(value)).ToHexString();
        }

        /// <summary>
        /// SHA1 算法
        /// </summary>
        /// <param name="value">需要加密的字串</param>
        /// <returns>加密结果</returns>
        public static string SHA1(this string value)
        {
            return ((HashAlgorithm)CryptoConfig.CreateFromName("SHA1")).ComputeHash(Encoding.UTF8.GetBytes(value)).ToHexString();
        }

    }
}
