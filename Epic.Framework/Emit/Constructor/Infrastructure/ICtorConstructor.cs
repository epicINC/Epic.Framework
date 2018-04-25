using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace Epic.Emit
{
    public interface ICtorConstructor : IConstructor
    {
        ConstructorBuilder Builder
        {
            get;
            set;
        }
    }
}
