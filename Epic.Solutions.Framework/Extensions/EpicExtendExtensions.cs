using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Extensions
{

    public interface IEpicExtendContainer<T>
    {
        T Value { get; set; }
    }

    public class EpicExtendContainer<T> : IEpicExtendContainer<T>
    {
        internal EpicExtendContainer(T value)
        {
            this.Value = value;
        }

        public T Value
        {
            get;
            set;
        }
    }



    public static class EpicExtendExtensions
    {
        public static IEpicExtendContainer<T> EpicExtend<T>(this T value)
        {
            return new EpicExtendContainer<T>(value);
        }

        static void WriteTest()
        {
            var value = 0;
            //value.CustomFor().TrueFor()
        }


        #region TrueFor


        public static K TrueFor<T, K>(this IEpicExtendContainer<T> value, bool condition, Func<T, K> trueAction)
        {
            return Ternary‎For<T, K>(value, condition, trueAction, () => default(K));
        }


        public static K TrueFor<T, K>(this IEpicExtendContainer<T> value, bool condition, Func<K> trueAction)
        {
            return Ternary‎For<T, K>(value, condition, trueAction, () => default(K));
        }

        public static K TrueFor<T, K>(this IEpicExtendContainer<T> value, Func<T, bool> condition, Func<T, K> trueAction)
        {
            return TrueFor(value, condition(value.Value), trueAction);
        }

        public static K TrueFor<T, K>(this IEpicExtendContainer<T> value, Func<bool> condition, Func<T, K> trueAction)
        {
            return TrueFor(value, condition(), trueAction);
        }
        public static K TrueFor<T, K>(this IEpicExtendContainer<T> value, Func<T, bool> condition, Func<K> trueAction)
        {
            return Ternary‎For<T, K>(value, condition(value.Value), trueAction, () => default(K));
        }
        public static K TrueFor<T, K>(this IEpicExtendContainer<T> value, Func<bool> condition, Func<K> trueAction)
        {
            return TernaryFor‎<T, K>(value, condition(), trueAction, () => default(K));
        }


        #endregion

        #region FalseFor

        public static K FalseFor<T, K>(this IEpicExtendContainer<T> value, bool condition, Func<T, K> falseAction)
        {
            return TernaryFor‎<T, K>(value, condition, () => default(K), falseAction);
        }

        public static K FalseFor<T, K>(this IEpicExtendContainer<T> value, bool condition, Func<K> falseAction)
        {
            return TernaryFor‎<T, K>(value, condition, () => default(K), falseAction);
        }


        public static K FalseFor<T, K>(this IEpicExtendContainer<T> value, Func<T, bool> condition, Func<T, K> falseAction)
        {
            return FalseFor(value, condition(value.Value), falseAction);
        }

        public static K FalseFor<T, K>(this IEpicExtendContainer<T> value, Func<bool> condition, Func<T, K> falseAction)
        {
            return FalseFor(value, condition(), falseAction);
        }

        public static K FalseFor<T, K>(this IEpicExtendContainer<T> value, Func<T, bool> condition, Func<K> falseAction)
        {
            return TernaryFor‎<T, K>(value, condition(value.Value), () => default(K), falseAction);
        }

        public static K FalseFor<T, K>(this IEpicExtendContainer<T> value, Func<bool> condition, Func<K> falseAction)
        {
            return Ternary‎For<T, K>(value, condition(), () => default(K), falseAction);
        }

        #endregion



        #region Ternary‎For

        public static K TernaryFor‎<T, K>(this IEpicExtendContainer<T> value, bool condition, Func<T, K> trueAction, Func<T, K> falseAction)
        {
            return condition ? trueAction(value.Value) : falseAction(value.Value);
        }

        public static K TernaryFor‎<T, K>(this IEpicExtendContainer<T> value, bool condition, K trueValue, K falseValue)
        {
            return condition ? trueValue : falseValue;
        }

        public static K Ternary‎For<T, K>(this IEpicExtendContainer<T> value, bool condition, K trueValue, Func<T, K> falseAction)
        {
            return condition ? trueValue : falseAction(value.Value);
        }

        public static K Ternary‎For<T, K>(this IEpicExtendContainer<T> value, bool condition, Func<T, K> trueAction, K falseValue)
        {
            return condition ? trueAction(value.Value) : falseValue;
        }



        public static K TernaryFor‎<T, K>(this IEpicExtendContainer<T> value, bool condition, Func<K> trueAction, Func<K> falseAction)
        {
            return condition ? trueAction() : falseAction();
        }

        public static K Ternary‎For<T, K>(this IEpicExtendContainer<T> value, bool condition, K trueValue, Func<K> falseAction)
        {
            return condition ? trueValue : falseAction();
        }

        public static K TernaryFor‎<T, K>(this IEpicExtendContainer<T> value, bool condition, Func<K> trueAction, K falseValue)
        {
            return condition ? trueAction() : falseValue;
        }

        public static K TernaryFor‎<T, K>(this IEpicExtendContainer<T> value, bool condition, Func<T, K> trueAction, Func<K> falseAction)
        {
            return condition ? trueAction(value.Value) : falseAction();
        }

        public static K Ternary‎For<T, K>(this IEpicExtendContainer<T> value, bool condition, Func<K> trueAction, Func<T, K> falseAction)
        {
            return condition ? trueAction() : falseAction(value.Value);
        }

        public static K Ternary‎For<T, K>(this IEpicExtendContainer<T> value, Func<T, bool> condition, Func<K> trueAction, Func<K> falseAction)
        {
            return TernaryFor‎(value, condition(value.Value), trueAction, falseAction);
        }

        public static K Ternary‎For<T, K>(this IEpicExtendContainer<T> value, Func<T, bool> condition, Func<T, K> trueAction, Func<T, K> falseAction)
        {
            return TernaryFor‎<T, K>(value, condition(value.Value), trueAction, falseAction);
        }

        public static K Ternary‎For<T, K>(this IEpicExtendContainer<T> value, Func<T, bool> condition, K trueValue, K falseValue)
        {
            return Ternary‎For(value, condition(value.Value), trueValue, falseValue);
        }

        public static K Ternary‎For<T, K>(this IEpicExtendContainer<T> value, Func<T, bool> condition, K trueValue, Func<T, K> falseAction)
        {
            return Ternary‎For(value, condition(value.Value), trueValue, falseAction);
        }

        public static K Ternary‎For‎<T, K>(this IEpicExtendContainer<T> value, Func<bool> condition, K trueValue, Func<T, K> falseAction)
        {
            return Ternary‎For(value, condition(), trueValue, falseAction);
        }

        public static K TernaryFor‎<T, K>(this IEpicExtendContainer<T> value, Func<T, bool> condition, Func<T, K> trueAction, K falseValue)
        {
            return TernaryFor‎(value, condition(value.Value), trueAction, falseValue);
        }



        public static K Ternary‎For<T, K>(this IEpicExtendContainer<T> value, Func<T, bool> condition, K trueValue, Func<K> falseAction)
        {
            return TernaryFor‎(value, condition(value.Value), trueValue, falseAction);
        }


        public static K Ternary‎For<T, K>(this IEpicExtendContainer<T> value, Func<T, bool> condition, Func<K> trueAction, K falseValue)
        {
            return Ternary‎For(value, condition(value.Value), trueAction, falseValue);
        }


        public static K TernaryFor‎<T, K>(this IEpicExtendContainer<T> value, Func<bool> condition, Func<K> trueAction, Func<K> falseAction)
        {
            return Ternary‎For(value, condition(), trueAction, falseAction);
        }

        public static K TernaryFor‎<T, K>(this IEpicExtendContainer<T> value, Func<bool> condition, Func<T, K> trueAction, Func<T, K> falseAction)
        {
            return TernaryFor‎<T, K>(value, condition(), trueAction, falseAction);
        }

        public static K TernaryFor‎<T, K>(this IEpicExtendContainer<T> value, Func<bool> condition, K trueValue, K falseValue)
        {
            return Ternary‎For(value, condition(), trueValue, falseValue);
        }

        public static K Ternary‎For<T, K>(this IEpicExtendContainer<T> value, Func<bool> condition, Func<T, K> trueAction, K falseValue)
        {
            return TernaryFor‎(value, condition(), trueAction, falseValue);
        }

        public static K Ternary‎For<T, K>(this IEpicExtendContainer<T> value, Func<bool> condition, K trueValue, Func<K> falseAction)
        {
            return Ternary‎For(value, condition(), trueValue, falseAction);
        }

        public static K Ternary‎For<T, K>(this IEpicExtendContainer<T> value, Func<bool> condition, Func<K> trueAction, K falseValue)
        {
            return TernaryFor‎(value, condition(), trueAction, falseValue);
        }



        public static K Ternary‎For<T, K>(this IEpicExtendContainer<T> value, Func<T, bool> condition, Func<T, K> trueAction, Func<K> falseAction)
        {
            return TernaryFor‎(value, condition(value.Value), trueAction, falseAction);
        }

        public static K Ternary‎For<T, K>(this IEpicExtendContainer<T> value, Func<bool> condition, Func<T, K> trueAction, Func<K> falseAction)
        {
            return TernaryFor‎(value, condition(), trueAction, falseAction);
        }

        public static K Ternary‎For<T, K>(this IEpicExtendContainer<T> value, Func<T, bool> condition, Func<K> trueAction, Func<T, K> falseAction)
        {
            return TernaryFor‎(value, condition(value.Value), trueAction, falseAction);
        }

        public static K Ternary‎For<T, K>(this IEpicExtendContainer<T> value, Func<bool> condition, Func<K> trueAction, Func<T, K> falseAction)
        {
            return Ternary‎For(value, condition(), trueAction, falseAction);
        }

        #endregion


    }
}
