using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Epic.Emit
{
    public class ModuleConstructor : BaseConstructor, IModuleConstructor
    {
        public ModuleBuilder Builder
        {
            get;
            set;
        }
    }

}
