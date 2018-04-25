using Epic.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

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
                return value.DefineAssembly(domain, assemblyName, AssemblyBuilderAccess.RunAndSave, path);
            }
            return value.DefineAssembly(domain, name, AssemblyBuilderAccess.Run);
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
            return value.Create<AssemblyConstructor>(e => {
                e.Name = name;
                e.Builder = domain.DefineDynamicAssembly(name, access, path);
                e.Context.Assembly = e;
            });
        }




        public static void Save(string path)
        {

        }

    }
}
