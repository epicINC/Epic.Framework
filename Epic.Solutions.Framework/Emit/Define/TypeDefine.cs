using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Emit
{
    public interface ITypeDefine : IGenericAttributesDefine<TypeAttributes>, IConstructor
    {
        string Name { get; set; }

        Type Parent { get; set; }

        Type[] Interfaces { get; set; }
    }

    public class TypeDefine : BaseConstructor, ITypeDefine
    {
        public string Name
        {
            get;
            set;
        }

        public TypeAttributes Attributes
        {
            get;
            set;
        }
        public Type Parent
        {
            get;
            set;
        }

        public Type[] Interfaces
        {
            get;
            set;
        }

    }
}
