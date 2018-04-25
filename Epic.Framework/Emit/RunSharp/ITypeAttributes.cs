using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Epic.Emit.RunSharp
{
    public interface ITypeAttributes : IContext
    {
        TypeAttributes Attributes
        {
            get;
            set;
        }
    }

    public static class ITypeAttributesExtensions
    {

        public static T Public<T>(this T value) where T :  ITypeAttributes
        {
            value.Attributes |= TypeAttributes.Public;
            return value;
        }

        public static T Private<T>(this T value) where T : ITypeAttributes
        {
            value.Attributes |= TypeAttributes.NotPublic;
            return value;
        }

        public static T Sealed<T>(this T value) where T : ITypeAttributes
        {
            value.Attributes |= TypeAttributes.Sealed;
            return value;
        }

        public static T Abstract<T>(this T value) where T : ITypeAttributes
        {
            value.Attributes |= TypeAttributes.Abstract;
            return value;
        }

        public static T AccessModifiers<T>(this T value, TypeAttributes attrs) where T :  ITypeAttributes
        {
            value.Attributes = attrs;
            return value;
        }
    }
}
