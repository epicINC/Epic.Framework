using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Epic.Emit.RunSharp
{
    public interface IMethodAttributes : IContext
    {
        MethodAttributes Attributes
        {
            get;
            set;
        }
    }


    public static class IMethodAttributesExtensions
    {

        public static T Public<T>(this T value) where T : IMethodAttributes
        {
            value.Attributes |= MethodAttributes.Public;
            return value;
        }

        public static T Private<T>(this T value) where T : IMethodAttributes
        {
            value.Attributes |= MethodAttributes.Private;
            return value;
        }

        public static T Final<T>(this T value) where T : IMethodAttributes
        {
            value.Attributes |= MethodAttributes.Final;
            return value;
        }

        public static T Virtual<T>(this T value) where T : IMethodAttributes
        {
            value.Attributes |= MethodAttributes.Virtual;
            return value;
        }

        public static T Static<T>(this T value) where T : IMethodAttributes
        {
            value.Attributes |= MethodAttributes.Static;
            return value;
        }

        public static T Abstract<T>(this T value) where T : IMethodAttributes
        {
            value.Attributes |= MethodAttributes.Abstract;
            return value;
        }

        public static T AccessModifiers<T>(this T value, MethodAttributes attrs) where T : IMethodAttributes
        {
            value.Attributes = attrs;
            return value;
        }
    }
}
