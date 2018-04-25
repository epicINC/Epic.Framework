using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Epic.Emit
{
    public interface ITypeChildConstructor : IConstructor
    {

    }


    public interface ITypeConstructor : IConstructor
    {
        TypeBuilder Builder { get; set; }
    }

    public class TypeConstructor : BaseBuilderConstructor<TypeBuilder>, ITypeConstructor
    {
    }
}
