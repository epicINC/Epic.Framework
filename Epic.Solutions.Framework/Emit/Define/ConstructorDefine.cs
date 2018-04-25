using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Emit
{
    public interface IConstructorDefine : IGenericAttributesDefine<MethodAttributes>, IConstructor
    {
        CallingConventions CallingConvention { get; set; }

        Type[] ParameterTypes { get; set; }

        Type[][] RequiredCustomModifiers { get; set; }

        Type[][] OptionalCustomModifiers { get; set; }
    }


    public class ConstructorDefine : BaseConstructor, IConstructorDefine
    {

        public MethodAttributes Attributes
        {
            get;
            set;
        }

        public CallingConventions CallingConvention
        {
            get;
            set;
        }

        public Type[] ParameterTypes
        {
            get;
            set;
        }

        public Type[][] RequiredCustomModifiers
        {
            get;
            set;
        }

        public Type[][] OptionalCustomModifiers
        {
            get;
            set;
        }
    }
}
