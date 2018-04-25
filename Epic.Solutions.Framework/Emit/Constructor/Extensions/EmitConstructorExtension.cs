using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Epic.Extensions;

namespace Epic.Emit
{
    public static class EmitConstructorExtension
    {

        public static IAssemblyConstructor DefineAssembly(this EmitConstructor value, string name)
        {
            return value.DefineAssembly(AppDomain.CurrentDomain, name);
        }

        public static IAssemblyConstructor DefineAssembly(this EmitConstructor value, AppDomain domain, string name)
        {
            if (name.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) || name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
            {
                var assemblyName = System.IO.Path.GetFileNameWithoutExtension(name);
                var fileName = System.IO.Path.GetFileName(name);
                var path = System.IO.Path.GetDirectoryName(name);


                return value.DefineAssembly(domain, assemblyName, AssemblyBuilderAccess.RunAndSave, path).Func(e =>
                    {
                        e.Name = assemblyName;
                        e.FileName = fileName;
                        return e;
                    });
            }
            return value.DefineAssembly(domain, name, AssemblyBuilderAccess.Run).Func(e =>
                {
                    e.Name = name;
                    e.FileName = name + ".dll";
                    return e;
                });
        }

        public static IAssemblyConstructor DefineAssembly(this EmitConstructor value, string name, AssemblyBuilderAccess access, string path = null)
        {
            return value.DefineAssembly(AppDomain.CurrentDomain, new AssemblyName(name), access, path);
        }

        public static IAssemblyConstructor DefineAssembly(this EmitConstructor value, AppDomain domain, string name, AssemblyBuilderAccess access, string path = null)
        {
            return value.DefineAssembly(domain, new AssemblyName(name), access, path);
        }

        public static IAssemblyConstructor DefineAssembly(this EmitConstructor value, AppDomain domain, AssemblyName name, AssemblyBuilderAccess access, string path = null)
        {
            return EmitConstructor.Create(value.Context, domain.DefineDynamicAssembly(name, access, path));
        }




        public static void Save(string path)
        {

        }

    }
}
