using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Emit
{
    public static class IAssemblyConstructorExtension
    {
        public static IModuleConstructor DefineDynamicModule(this IAssemblyConstructor value)
        {
            return DefineDynamicModule(value, value.FileName, null, false);
        }

        public static IModuleConstructor DefineDynamicModule(this IAssemblyConstructor value, string fileName)
        {
            return DefineDynamicModule(value, value.FileName, fileName, false);
        }

        public static IModuleConstructor DefineDynamicModule(this IAssemblyConstructor value, string name, string fileName, bool emitSymbolInfo = true)
        {
            return EmitConstructor.Create(value.Context, String.IsNullOrWhiteSpace(fileName) ? value.Builder.DefineDynamicModule(name, emitSymbolInfo) : value.Builder.DefineDynamicModule(name, fileName, emitSymbolInfo));
        }
    }
}
