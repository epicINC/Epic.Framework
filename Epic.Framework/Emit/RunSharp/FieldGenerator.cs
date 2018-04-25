using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Epic.Emit.RunSharp
{
    public class FieldGenerator : ITypeAttributes
    {
        public RunSharpContext Context
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public Type FieldType
        {
            get;
            set;
        }

        public TypeAttributes Attributes
        {
            get;
            set;
        }
        
    }

    public static class FieldGeneratorExtensions
    {
        // public 
        // Public().Name<string>("bane")
        public static FieldGenerator Name<T>(this FieldGenerator value, string name)
        {
            return Name(value, typeof(T), name);
        }

        public static FieldGenerator Name(this FieldGenerator value, Type type, string name)
        {
            value.FieldType = type;
            value.Name = name;
            return value;
        }
    }
}
