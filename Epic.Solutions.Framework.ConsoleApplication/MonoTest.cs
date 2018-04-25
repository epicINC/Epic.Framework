using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Epic.Utility;
using Mono.Collections.Generic;
using Epic.Extensions;


namespace Epic.Solutions.Framework.ConsoleApplication
{
    public class MonoTest
    {
        public static void Test()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Epic.Solutions.Framework.dll");
            var save = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Epic.Solutions.Framework_IL.dll");
            var assembly = AssemblyDefinition.ReadAssembly(path);
            var module = assembly.MainModule;
            SystemEnum = module.Import(typeof(System.Enum));

            //var types = assembly.Modules.SelectMany(e => e.Types).Where(
            //    e => e.GenericParameters.Any(k => k.Constraints.Any(y => y.Name == "IEnumConstraint")) || 
            //        e.Methods.Any(k => k.GenericParameters.Any(y => y.Constraints.Any(x => x.Name == "IEnumConstraint"))));

            var types = assembly.Modules.SelectMany(e => e.Types).Where(e => e.IsClass);

            ChangeConstraints(types.SelectMany(e => e.GenericParameters).Select(e => e.Constraints).Where(e => e.Any(k => k.Name == "IEnumConstraint")));
            ChangeConstraints(
                types.SelectMany(e => e.Methods).SelectMany(e => e.GenericParameters).Select(e => e.Constraints).Where(e => e.Any(k => k.Name == "IEnumConstraint"))
                );


            assembly.Write(save);
            // method.GenericParameters.First().Constraints.Last().Name
        }



        public static void ChangeMethod(Collection<MethodDefinition> collection)
        { 
        }

        static void ChangeConstraints(IEnumerable<Collection<TypeReference>> collection)
        {
            collection.ForEach(e => ChangeConstraints(e));
        }

        static void ChangeConstraints(Collection<TypeReference> collection)
        {

            if (IEnumConstraint == null)
                IEnumConstraint = collection.Single(e => e.Name == "IEnumConstraint");
            collection.Remove(IEnumConstraint);
            collection.Add(SystemEnum);
        }

        static TypeReference IEnumConstraint
        {
            get;
            set;
        }

        static TypeReference SystemEnum
        {
            get;
            set;
        }



    }
}
