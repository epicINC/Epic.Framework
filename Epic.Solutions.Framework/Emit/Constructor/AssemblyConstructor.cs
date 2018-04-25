using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.IO;

namespace Epic.Emit
{
    public interface IAssemblyConstructor : IConstructor
    {
        string Name
        {
            get;
            set;
        }

        AssemblyBuilder Builder
        {
            get;
            set;
        }

        string FileName
        {
            get;
            set;
        }

        void Save();
    }

    public class AssemblyConstructor : BaseBuilderConstructor<AssemblyBuilder>, IAssemblyConstructor
    {

        public string Name
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        public void Save()
        {
            if (this.Builder != null)
                this.Builder.Save(this.Builder.GetName().Name +".dll");
        }

    }
}
