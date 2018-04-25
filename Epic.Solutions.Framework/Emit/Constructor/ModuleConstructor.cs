using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Epic.Emit
{
    public interface IModuleConstructor : IConstructor
    {
        ModuleBuilder Builder
        {
            get;
        }

    }

    public class ModuleConstructor : BaseBuilderConstructor<ModuleBuilder>, IModuleConstructor
    {
    }

}
