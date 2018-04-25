using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.IO;
using Epic.Properties;

namespace Epic.Emit
{


    public class AssemblyConstructor : BaseConstructor, IAssemblyConstructor
    {

        public AssemblyName Name
        {
            get;
            set;
        }

        public AssemblyBuilder Builder
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        public void Save(string fileName)
        {
            if (this.Builder != null)
                this.Builder.Save(fileName);
        }

    }
}
