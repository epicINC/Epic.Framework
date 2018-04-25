using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Epic.Emit
{
    public class EmitContext : IEmitContext
    {

        public AssemblyName Name
        {
            get;
            set;
        }

        public IAssemblyConstructor Assembly
        {
            get;
            set;
        }

        public IModuleConstructor Module
        {
            get;
            set;
        }

        public ITypeConstructor Type
        {
            get;
            set;
        }

        public ICtorConstructor Ctor
        {
            get;
            set;
        }

        public IMethodConstructor Method
        {
            get;
            set;
        }

        public IPropertyConstructor Property
        {
            get;
            set;
        }

        public IFieldConstructor Field
        {
            get;
            set;
        }

    }

}
