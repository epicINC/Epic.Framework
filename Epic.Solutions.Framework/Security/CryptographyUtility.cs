using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Security.Cryptography;

namespace Epic.Security
{
    public static partial class Utility
    {

        public static class Hex
        {
            #region AzDG

            public static string AzDGEncode(string key, string value)
            {

                return Epic.Converter.ByteArrayConverter.ToHexString(Cryptography.AzDG.Encrypt(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(value)));
            }


            public static string AzDGDecode(string key, string value)
            {
                return Encoding.UTF8.GetString(Cryptography.AzDG.Decrypt(Encoding.UTF8.GetBytes(key), Epic.Converter.StringConverter.ToBitArray(value)));
            }

            #endregion

            #region XXTEA

            public static string XXTEAEncode(string key, string value)
            {

                return Epic.Converter.ByteArrayConverter.ToHexString(Cryptography.XXTEA.Encrypt(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(value)));
            }


            public static string XXTEADecode(string key, string value)
            {
                return Encoding.UTF8.GetString(Cryptography.XXTEA.Decrypt(Encoding.UTF8.GetBytes(key), Epic.Converter.StringConverter.ToBitArray(value)));
            }

            #endregion
        }

        public static class Base64
        {
            #region AzDG

            public static string AzDGEncode(string key, string value)
            {

                return Convert.ToBase64String(Cryptography.AzDG.Encrypt(Encoding.UTF8.GetBytes(value), Encoding.UTF8.GetBytes(key)));
            }


            public static string AzDGDecode(string key, string value)
            {
                return Encoding.UTF8.GetString(Cryptography.AzDG.Decrypt(Convert.FromBase64String(value), Encoding.UTF8.GetBytes(key)));
            }

            #endregion

            #region XXTEA

            public static string XXTEAEncode(string key, string value)
            {

                return Convert.ToBase64String(Cryptography.XXTEA.Encrypt(Encoding.UTF8.GetBytes(value), Encoding.UTF8.GetBytes(key)));
            }


            public static string XXTEADecode(string key, string value)
            {
                return Encoding.UTF8.GetString(Cryptography.XXTEA.Decrypt(Convert.FromBase64String(value), Encoding.UTF8.GetBytes(key)));
            }

            #endregion

        }




        /// <summary>
        /// 动网 MD5 算法
        /// </summary>
        /// <param name="encode">需要加密的字串</param>
        /// <returns>加密结果</returns>
        public static string DVMD5(string encode)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(encode);
            byte[] hashValue = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 4; i < 12; i++)
                sb.Append(hashValue[i].ToString("x2"));
            return sb.ToString();
        }


        /// <summary>
        /// MD5 算法
        /// </summary>
        /// <param name="value">需要加密的字串</param>
        /// <returns>加密结果</returns>
        public static string MD5(this string value)
        {
            return Epic.Converter.ByteArrayConverter.ToHexString(((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(Encoding.UTF8.GetBytes(value)));
        }

        /// <summary>
        /// SHA1 算法
        /// </summary>
        /// <param name="value">需要加密的字串</param>
        /// <returns>加密结果</returns>
        public static string SHA1(this string value)
        {
            return Epic.Converter.ByteArrayConverter.ToHexString(((HashAlgorithm)CryptoConfig.CreateFromName("SHA1")).ComputeHash(Encoding.UTF8.GetBytes(value)));
        }



    }
}
