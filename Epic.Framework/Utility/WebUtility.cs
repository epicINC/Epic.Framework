using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.IO;
using System.Web;

namespace Epic.Utility
{
    public static class WebUtility
    {
        #region Url Combine

        public static string UrlCombine(params string[] paths)
        {
            if (paths == null || paths.Length == 0) throw Error.ArgumentNull("paths");

            var result = new List<string>();
            foreach (var item in paths)
            {
                if (String.IsNullOrWhiteSpace(item)) continue;
                if (item[0] == '~')
                    result.Add(VirtualPathUtility.ToAbsolute(item).Trim('\\', '/'));
                else
                    result.Add(item.Trim('\\', '/'));
            }
            return String.Join<string>("/", result);
        }

        #endregion

        #region JavaScript Encode

        static unsafe int IndexOfJavaScriptEncodingChars(string s, int startPos)
        {
            int num = s.Length - startPos;
            fixed (char* str = s)
            {
                char* chPtr = str;
                char* chPtr2 = chPtr + startPos;
                while (num > 0)
                {
                    char ch = chPtr2[0];
                    if (ch <= '\\')
                    {
                        switch (ch)
                        {
                            case '\r':
                            case '\t':
                            case '"':
                            //case '\'':
                            case '\\':
                            case '\n':
                            case '\b':
                            case '\f':
                            case ' ':
                                return (s.Length - num);
                            case '=':
                            case '<':
                            case '>':
                                goto Label_0086;
                        }
                    }
                    else if ((ch >= '\x00a0') && (ch < 'Ā'))
                    {
                        return (s.Length - num);
                    }
                Label_0086:
                    chPtr2++;
                    num--;
                }
            }
            return -1;
        }

        public static string JavaScriptEncode(string value)
        {
            if (String.IsNullOrEmpty(value) || IndexOfJavaScriptEncodingChars(value, 0) == -1)
                return value;

            StringWriter output = new StringWriter(CultureInfo.InvariantCulture);
            JavaScriptEncode(value, output);
            return output.ToString();
        }

        public static unsafe void JavaScriptEncode(string value, TextWriter output)
        {
            if (value != null)
            {
                if (output == null)
                    throw new ArgumentNullException("output");
                int num = IndexOfJavaScriptEncodingChars(value, 0);
                if (num == -1)
                    output.Write(value);
                else
                {
                    int num2 = value.Length - num;
                    fixed (char* str = value)
                    {
                        char* chPtr = str;
                        char* chPtr2 = chPtr;
                        while (num-- > 0)
                        {
                            output.Write(chPtr2[0]);
                            chPtr2++;
                        }
 
                        while (num2-- > 0)
                        {
                            char ch = chPtr2[0];
                            chPtr2++;
                            if (ch <= '\\')
                            {
                                switch (ch)
                                {


                                    case '"':
                                        {
                                            output.Write("\\\"");
                                            continue;
                                        }
                                    case '\\':
                                        {
                                            output.Write(@"\\");
                                            continue;
                                        }
                                    //case '\'':
                                    //    {
                                    //        output.Write(@"&a");
                                    //        continue;
                                    //    }
                                    case '\r':
                                        {
                                            output.Write(@"\r");
                                            continue;
                                        }
                                    case '\t':
                                        {
                                            output.Write(@"\t");
                                            continue;
                                        }
                                    case '\n':
                                        {
                                            output.Write(@"\n");
                                            continue;
                                        }
                                    case '\b':
                                        {
                                            output.Write(@"\b");
                                            continue;
                                        }
                                    case '\f':
                                        {
                                            output.Write(@"\f");
                                            continue;
                                        }
                                    //case ' ':
                                }
                                output.Write(ch);
                                continue;
                            }
                            if ((ch >= '\x00a0') && (ch < 'Ā'))
                            {
                                output.Write("&#");
                                output.Write(((int)ch).ToString(NumberFormatInfo.InvariantInfo));
                                output.Write(';');
                            }
                            else
                            {
                                output.Write(ch);
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
