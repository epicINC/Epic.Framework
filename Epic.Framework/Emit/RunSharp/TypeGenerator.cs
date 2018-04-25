using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Epic.Extensions;

namespace Epic.Emit.RunSharp
{
    public class TypeGenerator
    {
        #region 属性

        public RunSharpContext Context
        {
            get;
            set;
        }

        internal string Name
        {
            get;
            set;
        }

        internal TypeAttributes Attributes
        {
            get;
            set;
        }

        internal Type Parent
        {
            get;
            set;
        }

        internal List<Type> Interfaces
        {
            get;
            set;
        }

        #endregion



        #region Access

        public TypeGenerator Public
        {
            get
            {
                this.Attributes |= TypeAttributes.Public;
                return this;
            }
        }

        public TypeGenerator Private
        {
            get
            {
                this.Attributes |= TypeAttributes.NotPublic;
                return this;
            }
        }

        public TypeGenerator Static
        {
            get
            {
                this.Attributes |= TypeAttributes.Abstract | TypeAttributes.AutoClass | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit;
                return this;
            }
        }

        public TypeGenerator Sealed
        {
            get
            {
                this.Attributes |= TypeAttributes.Sealed;
                return this;
            }
        }

        public TypeGenerator Abstract
        {
            get
            {
                this.Attributes |= TypeAttributes.Abstract;
                return this;
            }
        }

        public TypeGenerator AccessModifiers(TypeAttributes attrs)
        {
            this.Attributes = attrs;
            return this;
        }

        #endregion

        public TypeGenerator Class(string name)
        {
            this.Name = name;
            return this;
        }


        #region Inherit

        public TypeGenerator Inherit<T>()
        {
            return this.Inherit(typeof(T));
        }

        public TypeGenerator Inherit(Type value)
        {
            if (value.IsInterface)
            {
                if (this.Interfaces == null)
                    this.Interfaces = new List<Type>();

                this.Interfaces.Add(value);
            }
            else
            {
                // CS1721 multiple base classes
                if (this.Parent != null)
                    throw new ArgumentException("class 不能有多个基类 class1 和 class2");
                this.Parent = value;
            }

            return this;
        }

        public TypeGenerator Inherit(params Type[] value)
        {
            value.ForEach(e => this.Inherit(e));
            return this;
        }

        #endregion


        internal void Execute()
        {

        }

    }


    public static class TypeGeneratorExtensions
    {




        public static FieldGenerator Field(this TypeGenerator value)
        {

            return null;
        }
    }
}
