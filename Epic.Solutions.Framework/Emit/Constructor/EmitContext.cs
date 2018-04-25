using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Epic.Emit
{
    public interface IEmitContext
    {
        AssemblyName Name
        {
            get;
            set;
        }

        AssemblyBuilder Assembly
        {
            get;
            set;
        }

        ModuleBuilder Module
        {
            get;
            set;
        }

        TypeBuilder Type
        {
            get;
            set;
        }


        ConstructorBuilder Ctor
        {
            get;
            set;
        }

        MethodBuilder Method
        {
            get;
            set;
        }

        PropertyBuilder Property
        {
            get;
            set;
        }

        FieldBuilder Field
        {
            get;
            set;
        }

    }

    public class EmitContext : IEmitContext
    {

        public AssemblyName Name
        {
            get;
            set;
        }

        public AssemblyBuilder Assembly
        {
            get;
            set;
        }

        public ModuleBuilder Module
        {
            get;
            set;
        }

        public TypeBuilder Type
        {
            get;
            set;
        }

        public ConstructorBuilder Ctor
        {
            get;
            set;
        }

        public MethodBuilder Method
        {
            get;
            set;
        }

        public PropertyBuilder Property
        {
            get;
            set;
        }

        public FieldBuilder Field
        {
            get;
            set;
        }

    }

}
