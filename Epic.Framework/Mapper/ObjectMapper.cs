using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Mapper
{


    public abstract class ObjectMapper<Source, Dest> : IObjectMapper<Source,Dest>
    {
        public abstract Dest Convert(Source value);
    }
}
