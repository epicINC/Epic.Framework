using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;


namespace Epic.Solutions.BuildProcessor
{


    class Program
    {
        public Program()
        {
        }

        static List<IProcessor> Tasks()
        {
            var result = new List<IProcessor>();
            result.Add(new EnumProcessor());
            return result;
        }

        static void Main(string[] args)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Epic.Solutions.Framework.dll");
            var save = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Epic.Solutions.Framework.dll");
            var assembly = AssemblyDefinition.ReadAssembly(path);
            var module = assembly.MainModule;

            Tasks().ForEach(e => e.Process(module));

            assembly.Write(save);

        }
    }
}
