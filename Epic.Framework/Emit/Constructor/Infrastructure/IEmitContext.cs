using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Epic.Emit
{
    public interface IEmitContext
    {
        AssemblyName Name
        {
            get;
            set;
        }

        IAssemblyConstructor Assembly
        {
            get;
            set;
        }

        IModuleConstructor Module
        {
            get;
            set;
        }

        ITypeConstructor Type
        {
            get;
            set;
        }


        ICtorConstructor Ctor
        {
            get;
            set;
        }

        IMethodConstructor Method
        {
            get;
            set;
        }

        IPropertyConstructor Property
        {
            get;
            set;
        }

        IFieldConstructor Field
        {
            get;
            set;
        }

    }
}
