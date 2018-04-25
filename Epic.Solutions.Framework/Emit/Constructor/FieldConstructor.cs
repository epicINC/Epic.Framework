using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace Epic.Emit
{
    public interface IFieldConstructor : ITypeChildConstructor, IConstructor
    {
        FieldBuilder Builder
        {
            get;
            set;
        }
    }

    public class FieldConstructor : BaseBuilderConstructor<FieldBuilder>, IFieldConstructor
    {
    }
}
