using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Epic.Emit
{
    public interface IAssemblyConstructor : IConstructor
    {
        AssemblyName Name
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

        void Save(string fillName);
    }
}
