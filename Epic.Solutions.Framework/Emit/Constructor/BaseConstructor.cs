using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Emit
{
    public class BaseConstructor : IConstructor
    {
        public IEmitContext Context
        {
            get;
            set;
        }
    }

}
