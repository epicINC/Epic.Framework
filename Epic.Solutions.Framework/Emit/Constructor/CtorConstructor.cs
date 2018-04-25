using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Epic.Emit
{
    public interface ICtorConstructor : ITypeChildConstructor, IConstructor
    {
        ConstructorBuilder Builder
        {
            get;
            set;
        }
    }

    public class CtorConstructor : BaseBuilderConstructor<ConstructorBuilder>, ICtorConstructor
    {

    }


}
