using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Extensions
{
    /// <summary>
    /// 引用自 System.Data.Entity.ModelConfiguration.Utilities
    /// </summary>
    static class ObjectExtensions
    {
        #region Ternary‎

        private static K Ternary‎<T, K>(this T value, bool condition, Func<T, K> trueAction, Func<T, K> falseAction)
        {
            return condition ? trueAction(value) : falseAction(value);
        }

        private static K Ternary‎<T, K>(this T value, bool condition, K trueValue, K falseValue)
        {
            return condition ? trueValue : falseValue;
        }

        private static K Ternary‎<T, K>(this T value, bool condition, K trueValue, Func<T, K> falseAction)
        {
            return condition ? trueValue : falseAction(value);
        }

        private static K Ternary‎<T, K>(this T value, bool condition, Func<T, K> trueAction, K falseValue)
        {
            return condition ? trueAction(value) : falseValue;
        }

        private static K True<T, K>(this T value, bool condition, Func<T, K> trueAction)
        {
            return Ternary‎<T, K>(value, condition, trueAction, () => default(K));
        }

        private static K False<T, K>(this T value, bool condition, Func<T, K> falseAction)
        {
            return Ternary‎<T, K>(value, condition, () => default(K), falseAction);
        }

        private static K Ternary‎<T, K>(this T value, bool condition, Func<K> trueAction, Func<K> falseAction)
        {
            return condition ? trueAction() : falseAction();
        }

        private static K Ternary‎<T, K>(this T value, bool condition, K trueValue, Func<K> falseAction)
        {
            return condition ? trueValue : falseAction();
        }

        private static K Ternary‎<T, K>(this T value, bool condition, Func<K> trueAction, K falseValue)
        {
            return condition ? trueAction() : falseValue;
        }

        private static K True<T, K>(this T value, bool condition, Func<K> trueAction)
        {
            return Ternary‎<T, K>(value, condition, trueAction, () => default(K));
        }


        private static K False<T, K>(this T value, bool condition, Func<K> falseAction)
        {
            return Ternary‎<T, K>(value, condition, () => default(K), falseAction);
        }


        private static K Ternary‎<T, K>(this T value, bool condition, Func<T, K> trueAction, Func<K> falseAction)
        {
            return condition ? trueAction(value) : falseAction();
        }

        private static K Ternary‎<T, K>(this T value, bool condition, Func<K> trueAction, Func<T, K> falseAction)
        {
            return condition ? trueAction() : falseAction(value);
        }


        private static K Ternary‎<T, K>(this T value, Func<T, bool> condition, Func<K> trueAction, Func<K> falseAction)
        {
            return Ternary‎(value, condition(value), trueAction, falseAction);
        }

        private static K Ternary‎<T, K>(this T value, Func<T, bool> condition, Func<T, K> trueAction, Func<T, K> falseAction)
        {
            return Ternary‎<T, K>(value, condition(value), trueAction, falseAction);
        }

        private static K Ternary‎<T, K>(this T value, Func<T, bool> condition, K trueValue, K falseValue)
        {
            return Ternary‎(value, condition(value), trueValue, falseValue);
        }

        private static K Ternary‎<T, K>(this T value, Func<T, bool> condition, K trueValue, Func<T, K> falseAction)
        {
            return Ternary‎(value, condition(value), trueValue, falseAction);
        }

        private static K Ternary‎<T, K>(this T value, Func<bool> condition, K trueValue, Func<T, K> falseAction)
        {
            return Ternary‎(value, condition(), trueValue, falseAction);
        }


        private static K Ternary‎<T, K>(this T value, Func<T, bool> condition, Func<T, K> trueAction, K falseValue)
        {
            return Ternary‎(value, condition(value), trueAction, falseValue);
        }

        private static K True<T, K>(this T value, Func<T, bool> condition, Func<T, K> trueAction)
        {
            return True(value, condition(value), trueAction);
        }

        private static K True<T, K>(this T value, Func<bool> condition, Func<T, K> trueAction)
        {
            return True(value, condition(), trueAction);
        }


        private static K False<T, K>(this T value, Func<T, bool> condition, Func<T, K> falseAction)
        {
            return False(value, condition(value), falseAction);
        }

        private static K Ternary‎<T, K>(this T value, Func<T, bool> condition, K trueValue, Func<K> falseAction)
        {
            return Ternary‎(value, condition(value), trueValue, falseAction);
        }


        private static K Ternary‎<T, K>(this T value, Func<T, bool> condition, Func<K> trueAction, K falseValue)
        {
            return Ternary‎(value, condition(value), trueAction, falseValue);
        }
        private static K True<T, K>(this T value, Func<T, bool> condition, Func<K> trueAction)
        {
            return Ternary‎<T, K>(value, condition(value), trueAction, () => default(K));
        }
        private static K False<T, K>(this T value, Func<T, bool> condition, Func<K> falseAction)
        {
            return Ternary‎<T, K>(value, condition(value), () => default(K), falseAction);
        }


        private static K Ternary‎<T, K>(this T value, Func<bool> condition, Func<K> trueAction, Func<K> falseAction)
        {
            return Ternary‎(value, condition(), trueAction, falseAction);
        }

        private static K Ternary‎<T, K>(this T value, Func<bool> condition, Func<T, K> trueAction, Func<T, K> falseAction)
        {
            return Ternary‎<T, K>(value, condition(), trueAction, falseAction);
        }

        private static K Ternary‎<T, K>(this T value, Func<bool> condition, K trueValue, K falseValue)
        {
            return Ternary‎(value, condition(), trueValue, falseValue);
        }

        private static K Ternary‎<T, K>(this T value, Func<bool> condition, Func<T, K> trueAction, K falseValue)
        {
            return Ternary‎(value, condition(), trueAction, falseValue);
        }

        private static K False<T, K>(this T value, Func<bool> condition, Func<T, K> falseAction)
        {
            return False(value, condition(), falseAction);
        }

        private static K Ternary‎<T, K>(this T value, Func<bool> condition, K trueValue, Func<K> falseAction)
        {
            return Ternary‎(value, condition(), trueValue, falseAction);
        }

        private static K Ternary‎<T, K>(this T value, Func<bool> condition, Func<K> trueAction, K falseValue)
        {
            return Ternary‎(value, condition(), trueAction, falseValue);
        }

        private static K True<T, K>(this T value, Func<bool> condition, Func<K> trueAction)
        {
            return Ternary‎<T, K>(value, condition(), trueAction, () => default(K));
        }

        private static K False<T, K>(this T value, Func<bool> condition, Func<K> falseAction)
        {
            return Ternary‎<T, K>(value, condition(), () => default(K), falseAction);
        }

        private static K Ternary‎<T, K>(this T value, Func<T, bool> condition, Func<T, K> trueAction, Func<K> falseAction)
        {
            return Ternary‎(value, condition(value), trueAction, falseAction);
        }

        private static K Ternary‎<T, K>(this T value, Func<bool> condition, Func<T, K> trueAction, Func<K> falseAction)
        {
            return Ternary‎(value, condition(), trueAction, falseAction);
        }

        private static K Ternary‎<T, K>(this T value, Func<T, bool> condition, Func<K> trueAction, Func<T, K> falseAction)
        {
            return Ternary‎(value, condition(value), trueAction, falseAction);
        }

        private static K Ternary‎<T, K>(this T value, Func<bool> condition, Func<K> trueAction, Func<T, K> falseAction)
        {
            return Ternary‎(value, condition(), trueAction, falseAction);
        }

        #endregion
    }

    public static class ObjectPublicExtensions
    {

        public static bool IsNull(this object value)
        {
            return value == null;
        }

    

        
    }
}
