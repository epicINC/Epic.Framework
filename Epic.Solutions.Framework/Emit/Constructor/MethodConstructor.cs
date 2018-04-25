using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Epic.Emit
{
    public interface IMethodConstructor : ITypeChildConstructor, IConstructor
    {
        MethodBuilder Builder
        {
            get;
            set;
        }
    }

    public class MethodConstructor : BaseBuilderConstructor<MethodBuilder>, IMethodConstructor
    {
    }

}
