using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Extensions
{
    public static class StringExtensions
    {
        #region Convert

 




        #endregion




        public static string Repeat(this char text, int count)
        {
            return new String(text, count);
        }

        public static string Repeat(this string text, int count)
        {
            var result = String.Empty;
            for (int i = 0; i < count; i++)
            {
                result += text;
            }
            return result;
        }

        #region Filter


        /// <summary>
        /// 确定此实例的末尾是否与指定的字符匹配.
        /// </summary>
        /// <param name="text">实例</param>
        /// <param name="value">用来比较的 字符</param>
        /// <returns>如果 value 与此实例的末尾匹配，则为 true；否则为 false。</returns>
        public static bool EndsWith(this string text, char value)
        {
            if (String.IsNullOrWhiteSpace(text)) return false;
            return text[text.Length - 1] == value;
        }


        public static bool StartsWith(this string value, params string[] collection)
        {
            return value.Filter(e => value.StartsWith(e), collection);
        }

        public static bool StartsWith(this string value, IEnumerable<string> collection)
        {
            return value.Filter(e => value.StartsWith(e), collection);
        }


        public static bool Contains(this string value, params string[] collection)
        {
            return value.Filter(e => value.Contains(e), collection);
        }

        public static bool Contains(this string value, IEnumerable<string> collection)
        {
            return value.Filter(e => value.Contains(e), collection);
        }

        public static bool EndsWith(this string value, params string[] collection)
        {
            return value.Filter(e => value.EndsWith(e), collection);
        }

        public static bool EndsWith(this string value, IEnumerable<string> collection)
        {
            return value.Filter(e => value.EndsWith(e), collection);
        }

        public static bool Filter(this string value, Predicate<string> func, IEnumerable<string> collection)
        {
            foreach (var item in collection)
                if (func(item)) return true;
            return false;
        }

        #endregion

        /// <summary>
        /// 截断此实例前后指定长度的字符
        /// </summary>
        /// <param name="text">实例</param>
        /// <param name="length">需要截断的长度</param>
        /// <returns>截断后的文本</returns>
        public static string Trim(this string text, int length)
        {
            return Trim(text, length, length);
        }

        /// <summary>
        /// 截断此实例前后指定长度的字符
        /// </summary>
        /// <param name="text">实例</param>
        /// <param name="startLength">开始长度</param>
        /// <param name="endLength">结束长度</param>
        /// <returns>截断后的文本</returns>
        public static string Trim(this string text, int startLength, int endLength)
        {
            if (String.IsNullOrEmpty(text)) return text;
            if (startLength < 0)
                throw new ArgumentOutOfRangeException("startLength");
            if (endLength < 0)
                throw new ArgumentOutOfRangeException("startLength");
            if ((startLength + endLength) > text.Length)
                throw new ArgumentOutOfRangeException("startLength + endLength");

            return text.Substring(startLength, text.Length - startLength - endLength);
        }


        /// <summary>
        /// 截断此实例开始指定长度的字符
        /// </summary>
        /// <param name="text">实例</param>
        /// <param name="length">截断长度</param>
        /// <returns>截断后的文本</returns>
        public static string TrimStart(this string text, int length)
        {
            if (String.IsNullOrEmpty(text)) return text;
            return text.Substring(length);
        }

        public static string TrimStartBy(this string text, char symbol)
        {
            return text.TrimStart(text.IndexOf(symbol));
        }



        #region Remove


        public static string RemoveEnd(this string text, string value)
        {
            var offset = text.IndexOf(value);
            if (offset != -1)
                return text.Substring(0, offset);
            else
                return text;
        }



        #endregion

        #region TrimEnd

        public static string TrimEnd(this string text, string key)
        {
            return text.TrimEnd(key.ToCharArray());
        }
        /// <summary>
        /// 截断此实例末尾指定长度的字符
        /// </summary>
        /// <param name="text">实例</param>
        /// <param name="length">截断长度</param>
        /// <returns>截断后的文本</returns>
        public static string TrimEnd(this string text, int length)
        {
            if (String.IsNullOrEmpty(text)) return text;
            return text.Substring(0, text.Length - length);
        }

        /// <summary>
        /// 截断此实例末尾指定字符后的字符
        /// </summary>
        /// <param name="text">实例</param>
        /// <param name="symbol">字符</param>
        /// <returns>截断后的文本</returns>
        public static string TrimEndBy(this string text, char symbol)
        {
            return text.Substring(0, text.LastIndexOf(symbol));
        }

        #endregion


        #region Sub

        public static string Substring(this string text, string start, string end)
        {
            var offsetStart = text.IndexOf(start);
            var offsetEnd = text.IndexOf(end, offsetStart + start.Length) + end.Length;

            return text.Substring(offsetStart, offsetEnd - offsetStart);
        }

        /// <summary>
        /// 截取不包含 start 和 end 的正文内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string SubContent(this string text, string start, string end)
        {
            return SubContent(text, 0, start, end);
        }

        public static string SubContent(this string text, int offset, string startKey, string endKey)
        {
            var offsetStart = text.IndexOf(startKey, offset) + startKey.Length;
            if (offsetStart == -1) return null;

            var offsetEnd = text.LastIndexOf(endKey, offsetStart);
            if (offsetEnd == -1) return null;

            return text.Substring(offsetStart, offsetEnd - offsetStart);
        }

        public static string[] SubContent(this string text, params string[] keys)
        {
            return SubContent(text, 0, keys);
        }


        public static string[] SubContent(this string text, int offset, params string[] keys)
        {
            var result = new List<string>();

            int offsetStart, offsetEnd;
            string startKey, endKey;
            for (int i = 0; i < keys.Length; i += 2)
            {
                startKey = keys[i];
                endKey = keys[i + 1];

                offsetStart = text.IndexOf(startKey, offset) + startKey.Length;
                if (offsetStart == -1) return null;

                offsetEnd = text.IndexOf(endKey, offsetStart);
                if (offsetEnd == -1) return null;

                offset = offsetEnd + endKey.Length;


                result.Add(text.Substring(offsetStart, offsetEnd - offsetStart));

                
            }

            return result.ToArray();

        }


        #endregion

        #region ToID 转换方法

        public static int ToID(this string value)
        {
            return ToInt(value, 1, null);
        }


        #endregion

        #region ToShort

        public static short ToShort(this string value)
        {
            return ToShort(value, Int16.TryParse, null, null, null, null);
        }

        public static short ToShort(this string value, Predicate<short> filter, short? defaultValue, short? min, short? max)
        {
            return ToShort(value, Int16.TryParse, filter, defaultValue, min, max);
        }

        public static short ToShort(this string value, ParseAction<string, short> action, Predicate<short> filter, short? defaultValue, short? min, short? max)
        {
            if (String.IsNullOrEmpty(value)) goto DefaultValue;
            short result;

            if (action == null) throw new ArgumentNullException("action");
            if (!action(value, out result)) goto DefaultValue;

            if (min != null && result < min.Value) goto DefaultValue;
            if (max != null && result > max.Value) goto DefaultValue;
            if (filter != null && !filter(result)) goto DefaultValue;

            return result;

        DefaultValue:
            return defaultValue != null ? defaultValue.Value : (short)0;
        }

        #endregion

        #region ToInt Method

        /// <summary>
        /// 使用默认转换方法, 将 String 转换为等效的 32 位有符号整数
        /// </summary>
        /// <param name="value">包含要转换 String</param>
        /// <returns>与 value 的值等效的 32 位有符号整数</returns>
        public static int ToInt(this string value)
        {
            return ToInt(value, Int32.TryParse, null, null, null, null);
        }

        /// <summary>
        /// 使用默认转换方法, 将 String 转换为等效的 32 位有符号整数
        /// </summary>
        /// <param name="value">包含要转换 String</param>
        /// <param name="filter">验证委托</param>
        /// <returns>与 value 的值等效的 32 位有符号整数</returns>
        public static int ToInt(this string value, Predicate<int> filter)
        {
            return ToInt(value, Int32.TryParse, filter, null, null, null);
        }

        /// <summary>
        /// 使用默认转换方法, 将 String 转换为等效的 32 位有符号整数
        /// </summary>
        /// <param name="value">包含要转换 String</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>与 value 的值等效的 32 位有符号整数</returns>
        public static int ToInt(this string value, int? min, int? max)
        {
            return ToInt(value, Int32.TryParse, null, null, min, max);
        }

        /// <summary>
        /// 使用默认转换方法, 将 String 转换为等效的 32 位有符号整数
        /// </summary>
        /// <param name="value">包含要转换 String</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>与 value 的值等效的 32 位有符号整数</returns>
        public static int ToInt(this string value, int? defaultValue, int? min, int? max)
        {
            return ToInt(value, Int32.TryParse, null, defaultValue, min, max);
        }

        /// <summary>
        /// 使用默认转换方法, 将 String 转换为等效的 32 位有符号整数
        /// </summary>
        /// <param name="value">包含要转换 String</param>
        /// <param name="filter">验证委托</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>与 value 的值等效的 32 位有符号整数</returns>
        public static int ToInt(this string value, Predicate<int> filter, int? defaultValue, int? min, int? max)
        {
            return ToInt(value, Int32.TryParse, filter, defaultValue, min, max);
        }

        /// <summary>
        /// 将 String 转换为等效的 32 位有符号整数
        /// </summary>
        /// <param name="value">包含要转换 String</param>
        /// <param name="action">转换委托</param>
        /// <param name="filter">验证委托</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>与 value 的值等效的 32 位有符号整数</returns>
        public static int ToInt(this string value, ParseAction<string, int> action, Predicate<int> filter, int? defaultValue, int? min, int? max)
        {
            if (String.IsNullOrEmpty(value)) goto DefaultValue;
            int result;

            if (action == null) throw new ArgumentNullException("action");
            if (!action(value, out result)) goto DefaultValue;

            if (min != null && result < min.Value) goto DefaultValue;
            if (max != null && result > max.Value) goto DefaultValue;
            if (filter != null && !filter(result)) goto DefaultValue;

            return result;

        DefaultValue:
            return defaultValue != null ? defaultValue.Value : 0;
        }

        #endregion

        #region ToLong Method

        public static long ToLong(this string value)
        {
            return ToLong(value, Int64.TryParse, null, null, null, null);
        }

        public static long ToLong(this string value, Predicate<long> filter, long? defaultValue, long? min, long? max)
        {
            return ToLong(value, Int64.TryParse, filter, defaultValue, min, max);
        }

        public static long ToLong(this string value, ParseAction<string, long> action, Predicate<long> filter, long? defaultValue, long? min, long? max)
        {
            if (String.IsNullOrEmpty(value)) goto DefaultValue;
            long result;

            if (action == null) throw new ArgumentNullException("action");
            if (!action(value, out result)) goto DefaultValue;

            if (min != null && result < min.Value) goto DefaultValue;
            if (max != null && result > max.Value) goto DefaultValue;
            if (filter != null && !filter(result)) goto DefaultValue;

            return result;

        DefaultValue:
            return defaultValue != null ? defaultValue.Value : 0;
        }

        #endregion

        #region ToText Method

        public static string ToSafeXml(this string value)
        {
            if (String.IsNullOrEmpty(value)) goto DefaultValue;
            return Utility.RegexLib.InvalidXmlChar.Replace(value, String.Empty);
        DefaultValue:
            return value;
        }

        public static string ToSafeQuery(this string value)
        {
            if (String.IsNullOrEmpty(value)) goto DefaultValue;
            return value.Replace("'", "''");
        DefaultValue:
            return value;
        }

        #endregion

        /// <summary>
        /// 判断字符串是否为数字
        /// </summary>
        /// <param name="value">需要判断的字符串</param>
        /// <returns>是否为数字</returns>
        public static bool IsNumber(this string value)
        {
            foreach (char item in value)
            {
                if (!Char.IsDigit(item)) return false;
            }
            return true;
            //int result;
            //return Int32.TryParse(value, out result);
        }




        #region Array 数组方法

        /// <summary>
        /// 分割数组
        /// </summary>
        /// <param name="value">需要分割的字符串</param>
        /// <param name="separator">分割文本</param>
        /// <returns>分割结果</returns>
        public static string[] Split(this string value, params string[] separator)
        {

            return value.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        #region ToByteArray

        public static byte[] ToByteArray(this string value, string separator)
        {
            byte[] result;
            value.Split(separator).TryParse(out result);
            return result;
        }
        public static byte[] ToByteArray(this string value, char separator)
        {
            byte[] result;
            value.Split(separator).TryParse(out result);
            return result;
        }

        #endregion

        #region ToIntArray

        public static int[] ToIntArray(this string value, char separator)
        {
            int[] result;
            value.Split(separator).TryParse(out result);
            return result;
        }

        public static int[] ToIntArray(this string value, string separator)
        {
            int[] result;
            value.Split(separator).TryParse(out result);
            return result;
        }

        public static int[] ToIDArray(this string value, char separator)
        {
            int[] result;
            value.Split(separator).TryParse(out result, IntExtensions.TryParseID, true);
            return result;
        }

        public static int[] ToIDArray(this string value, string separator)
        {
            int[] result;
            value.Split(separator).TryParse(out result, IntExtensions.TryParseID, true);
            return result;
        }

        #endregion

        #endregion


        #region ToEnum Method

        public static T ToEnum<T>(this string value) where T : struct
        {
            return ToEnum<T>(value, Enum.TryParse);
        }

        public static T ToEnum<T>(this string value, ParseAction<string, T> action) where T : struct
        {
            if (String.IsNullOrEmpty(value)) goto DefaultValue;
            if (action == null) throw new ArgumentNullException("action");
            T result;
            if (!action(value, out result)) goto DefaultValue;

            return result;
        DefaultValue:
            return default(T);
        }


        public static T[] ToEnumArray<T>(this string value, string separator) where T : struct
        {
            T[] result;
            value.Split(separator).TryParseEnum(out result, true);
            return result;
        }

        #endregion


    }
}
