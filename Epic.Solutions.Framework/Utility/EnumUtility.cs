using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Epic.Caching;
using Epic.Extensions;
using Epic.Components;

namespace Epic.Utility
{




    public static class EnumUtility
    {
        public static bool IsFlags<T>() where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.IsFlags;
        }

        public static Type Type<T>() where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Type;
        }

        public static Type UnderlyingType<T>() where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.UnderlyingType;
        }


        public static T And<T>(T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.And(left, right);
        }

        public static T And<T>(T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.And(left, right);
        }

        public static T And<T>(T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.And(left, right);
        }

        public static T And<T>(T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.And(left, right);
        }


        public static T Or<T>(T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Or(left, right);
        }

        public static T Or<T>(T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Or(left, right);
        }

        public static T Or<T>(T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Or(left, right);
        }

        public static T Or<T>(T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Or(left, right);
        }


        public static T Xor<T>(T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Xor(left, right);
        }

        public static T Xor<T>(T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Xor(left, right);
        }

        public static T Xor<T>(T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Xor(left, right);
        }

        public static T Xor<T>(T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Xor(left, right);
        }



        public static T Not<T>(T value) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Not(value);
        }

        public static T Not<T>(byte value) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Not(value);
        }

        public static T Not<T>(int value) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Not(value);
        }

        public static T Not<T>(long value) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Not(value);
        }



        public static T Set<T>(T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Set(left, right);
        }

        public static T Set<T>(T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Set(left, right);
        }

        public static T Set<T>(T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Set(left, right);
        }

        public static T Set<T>(T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Set(left, right);
        }



        public static T Add<T>(T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Add(left, right);
        }

        public static T Add<T>(T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Add(left, right);
        }

        public static T Add<T>(T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Add(left, right);
        }

        public static T Add<T>(T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Add(left, right);
        }


        public static T Remove<T>(T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Remove(left, right);
        }

        public static T Remove<T>(T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Remove(left, right);
        }

        public static T Remove<T>(T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Remove(left, right);
        }

        public static T Remove<T>(T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.Remove(left, right);
        }



        public static bool HasValue<T>(T left, T right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.HasValue(left, right);
        }

        public static bool HasValue<T>(T left, byte right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.HasValue(left, right);
        }

        public static bool HasValue<T>(T left, int right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.HasValue(left, right);
        }

        public static bool HasValue<T>(T left, long right) where T : struct, IEnumConstraint
        {
            return EnumInternal<T>.HasValue(left, right);
        }
    }





}
