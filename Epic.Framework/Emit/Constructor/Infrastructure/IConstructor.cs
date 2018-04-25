using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Emit
{
    public interface IConstructor
    {
        IEmitContext Context
        {
            get;
            set;
        }
    }
}
