using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Emit
{
    public class PropertyConstructor : BaseConstructor, IPropertyConstructor
    {
        public PropertyBuilder Builder
        {
            get;
            set;
        }
    }
}

