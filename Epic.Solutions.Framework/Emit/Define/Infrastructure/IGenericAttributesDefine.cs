using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Emit
{
    public interface IGenericAttributesDefine<T>
    {
        T Attributes { get; set; }
    }
}
