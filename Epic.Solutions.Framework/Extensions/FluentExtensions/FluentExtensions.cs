using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Extensions.FluentExtensions
{
    public static class FluentExtensions
    {
        public static IFluentContainer<T> Fluent<T>(this T value)
        {
            return FluentContainer.Create(value);
        }
        public static IFluentRemoveContainer<T> Remove<T>(this IFluentContainer<T> value)
        {
            return FluentContainer.CreateRemove(value);
        }


        public static IFluentContainer<T> Do<T>(this IFluentContainer<T> value, Action<T> action)
        {
            action(value.Value);
            return value;
        }

        public static K Func<T, K>(this IFluentContainer<T> value, Func<T, K> action)
        {
            return action(value.Value);
        }


        public static IFluentRemoveContainer<T> Do<T>(this IFluentRemoveContainer<T> value, Action<T> action)
        {
            action(value.Value);
            return value;
        }

        public static K Func<T, K>(this IFluentRemoveContainer<T> value, Func<T, K> action)
        {
            return action(value.Value);
        }
    }
}
