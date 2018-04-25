using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace Epic.Emit
{
    public interface IPropertyConstructor : IConstructor
    {
        PropertyBuilder Builder
        {
            get;
            set;
        }
    }
}
