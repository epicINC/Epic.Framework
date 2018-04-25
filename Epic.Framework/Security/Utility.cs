using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Security.Cryptography;

namespace Epic.Security
{
    public static class Utility
    {

        public static class Hex
        {
            #region AzDG

            public static string AzDGEncode(string key, string value)
            {

                return ToHexString(Cryptography.AzDG.Encrypt(utf8.GetBytes(value), utf8.GetBytes(key)));
            }


            public static string AzDGDecode(string key, string value)
            {
                return utf8.GetString(Cryptography.AzDG.Decrypt(value.HexToBitArray(), utf8.GetBytes(key)));
            }

            #endregion

            #region XXTEA

            public static string XXTEAEncode(string key, string value)
            {

                return ToHexString(Cryptography.XXTEA.Encrypt(utf8.GetBytes(value), utf8.GetBytes(key)));
            }


            public static string XXTEADecode(string key, string value)
            {
                return utf8.GetString(Cryptography.XXTEA.Decrypt(value.HexToBitArray(), utf8.GetBytes(key)));
            }

            #endregion
        }

        public static class Base64
        {
            #region AzDG

            public static string AzDGEncode(string key, string value)
            {

                return Convert.ToBase64String(Cryptography.AzDG.Encrypt(utf8.GetBytes(value), utf8.GetBytes(key)));
            }


            public static string AzDGDecode(string key, string value)
            {
                return utf8.GetString(Cryptography.AzDG.Decrypt(Convert.FromBase64String(value), utf8.GetBytes(key)));
            }

            #endregion

            #region XXTEA

            public static string XXTEAEncode(string key, string value)
            {

                return Convert.ToBase64String(Cryptography.XXTEA.Encrypt(utf8.GetBytes(value), utf8.GetBytes(key)));
            }


            public static string XXTEADecode(string key, string value)
            {
                return utf8.GetString(Cryptography.XXTEA.Decrypt(Convert.FromBase64String(value), utf8.GetBytes(key)));
            }

            #endregion

        }
        static Encoding utf8 = System.Text.Encoding.UTF8;








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
            return ToHexString(((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(Encoding.UTF8.GetBytes(value)));
        }

        /// <summary>
        /// SHA1 算法
        /// </summary>
        /// <param name="value">需要加密的字串</param>
        /// <returns>加密结果</returns>
        public static string SHA1(this string value)
        {
            return ToHexString(((HashAlgorithm)CryptoConfig.CreateFromName("SHA1")).ComputeHash(Encoding.UTF8.GetBytes(value)));
        }


        #region Salt

        /// <summary>
        /// 生成随机字符
        /// </summary>
        /// <param name="length">字符长度</param>
        /// <returns>byte[] 数组</returns>
        public static byte[] SaltCustom(int length)
        {
            byte[] data = new byte[length];
            new RNGCryptoServiceProvider().GetBytes(data);
            return data;
        }

        /// <summary>
        /// 生成随机字符
        /// </summary>
        /// <param name="length">字符长度</param>
        /// <returns>生成结果</returns>
        public static string Salt(int length)
        {
            return ToHexString(SaltCustom(length / 2));
        }

        #endregion


        #region HexString <> Bit[]

        ///// <summary>
        ///// 16进制文本转换为 byte[].
        ///// </summary>
        ///// <param name="HexString">需要转换的 16进制文本.</param>
        ///// <returns>转换后得到的 byte[]</returns>
        //public static byte[] HexToBitArray(this string HexString)
        //{

        //    //return Enumerable.Range(0, hex.Length)
        //    //         .Where(x => x % 2 == 0)
        //    //         .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
        //    //         .ToArray();

        //    int length = HexString.Length;
        //    byte[] bytes = new byte[length / 2];
        //    for (int i = 0; i < length; i += 2)
        //    {
        //        bytes[i / 2] = System.Convert.ToByte(HexString.Substring(i, 2), 16);
        //    } return bytes;
        //}

        ///// <summary>
        ///// 将 byte[] 转换为 16进制文本.
        ///// </summary>
        ///// <param name="buf">需要转换的 byte[]</param>
        ///// <returns>转换后得到的 16进制文本.</returns>
        //public static string ToHexString(this byte[] buf)
        //{
        //    return BitConverter.ToString(buf).Replace("-", String.Empty);
        //}


       // public static string ByteArrayToHexString(byte[] Bytes)
       // {
       //     StringBuilder Result = new StringBuilder(Bytes.Length * 2);
       //     string HexAlphabet = "0123456789ABCDEF";

       //     foreach (byte B in Bytes)
       //     {
       //         Result.Append(HexAlphabet[(int)(B >> 4)]);
       //         Result.Append(HexAlphabet[(int)(B & 0xF)]);
       //     }

       //     return Result.ToString();
       // }

       // public static byte[] HexStringToByteArray(string Hex)
       // {
       //     byte[] Bytes = new byte[Hex.Length / 2];
       //     int[] HexValue = new int[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 
       //0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
       //0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

       //     for (int x = 0, i = 0; i < Hex.Length; i += 2, x += 1)
       //     {
       //         Bytes[x] = (byte)(HexValue[Char.ToUpper(Hex[i + 0]) - '0'] << 4 |
       //                           HexValue[Char.ToUpper(Hex[i + 1]) - '0']);
       //     }

       //     return Bytes;
       // }


        /// <summary>
        /// http://stackoverflow.com/questions/623104/byte-to-hex-string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] HexToBitArray(this string str)
        {
            if (str.Length == 0 || str.Length % 2 != 0)
                return new byte[0];

            byte[] buffer = new byte[str.Length / 2];
            char c;
            for (int bx = 0, sx = 0; bx < buffer.Length; ++bx, ++sx)
            {
                // Convert first half of byte
                c = str[sx];
                buffer[bx] = (byte)((c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0')) << 4);

                // Convert second half of byte
                c = str[++sx];
                buffer[bx] |= (byte)(c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0'));
            }

            return buffer;
        }


        public static string ToHexString(this byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2];

            byte b;

            for (int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx)
            {
                b = ((byte)(bytes[bx] >> 4));
                c[cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);

                b = ((byte)(bytes[bx] & 0x0F));
                c[++cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);
            }

            return new string(c);
        }




        #endregion
    }
}
