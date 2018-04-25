using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Utility;
using Epic.Components;

namespace Epic.Extensions
{
    public static class EnumExtensions
    {

        public static T And<T>(this T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.And(left, right);
        }

        public static T And<T>(this T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.And(left, right);
        }

        public static T And<T>(this T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.And(left, right);
        }

        public static T And<T>(this T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.And(left, right);
        }


        public static T Or<T>(this T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Or(left, right);
        }

        public static T Or<T>(this T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Or(left, right);
        }

        public static T Or<T>(this T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Or(left, right);
        }

        public static T Or<T>(this T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Or(left, right);
        }


        public static T Xor<T>(this T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Xor(left, right);
        }

        public static T Xor<T>(this T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Xor(left, right);
        }

        public static T Xor<T>(this T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Xor(left, right);
        }

        public static T Xor<T>(this T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Xor(left, right);
        }



        public static T Not<T>(this T value) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Not(value);
        }

        public static T Not<T>(this byte value) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Not(value);
        }

        public static T Not<T>(this int value) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Not(value);
        }

        public static T Not<T>(this long value) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Not(value);
        }



        public static T Set<T>(this T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Set(left, right);
        }

        public static T Set<T>(this T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Set(left, right);
        }

        public static T Set<T>(this T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Set(left, right);
        }

        public static T Set<T>(this T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Set(left, right);
        }



        public static T Set<T>(this T left, T right, bool addOrRemove) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Set(left, right, addOrRemove);
        }

        public static T Set<T>(this T left, byte right, bool addOrRemove) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Set(left, right, addOrRemove);
        }

        public static T Set<T>(this T left, int right, bool addOrRemove) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Set(left, right, addOrRemove);
        }

        public static T Set<T>(this T left, long right, bool addOrRemove) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Set(left, right, addOrRemove);
        }



        public static T Add<T>(this T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Add(left, right);
        }

        public static T Add<T>(this T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Add(left, right);
        }

        public static T Add<T>(this T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Add(left, right);
        }

        public static T Add<T>(this T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Add(left, right);
        }


        public static T Remove<T>(this T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Remove(left, right);
        }

        public static T Remove<T>(this T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Remove(left, right);
        }

        public static T Remove<T>(this T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Remove(left, right);
        }

        public static T Remove<T>(this T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Remove(left, right);
        }



        public static bool HasValue<T>(this T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.HasValue(left, right);
        }

        public static bool HasValue<T>(this T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.HasValue(left, right);
        }

        public static bool HasValue<T>(this T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.HasValue(left, right);
        }

        public static bool HasValue<T>(this T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.HasValue(left, right);
        }


    }
}
