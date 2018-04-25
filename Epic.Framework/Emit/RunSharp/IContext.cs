using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Emit.RunSharp
{
    public interface IContext
    {
        RunSharpContext Context
        {
            get;
            set;
        }
    }
}
