using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Components
{
    /// <summary>
    /// 简单配置文件读取
    /// </summary>
    public interface ISimpleConfig
    {

    }

    public static class SimpleConfigExtension
    {
        public static void LoadConfig<T>(this T t, string value, Action<T, string> action) where T : ISimpleConfig
        {
            if (!String.IsNullOrWhiteSpace(value))
                action(t, value);
        }

        public static void LoadConfig<T>(this T t, string value, Action<T, string> trueAction, Action<T> falseAction) where T : ISimpleConfig
        {
            if (!String.IsNullOrWhiteSpace(value))
                trueAction(t, value);
            else
                falseAction(t);
        }
    }
}
