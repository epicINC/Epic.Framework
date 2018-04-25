using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Emit
{
    public interface IPropertyConstructor : ITypeChildConstructor, IConstructor
    {
        PropertyBuilder Builder
        {
            get;
            set;
        }
    }

    public class PropertyConstructor : BaseBuilderConstructor<PropertyBuilder>, IPropertyConstructor
    {
    }
}

