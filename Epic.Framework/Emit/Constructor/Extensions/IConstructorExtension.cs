using Epic.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Emit
{
    public static class IConstructorExtension
    {
        internal static T Create<T>(this IConstructor value) where T : IConstructor, new()
        {
            return new T() { Context = value.Context };
        }

        internal static T Create<T>(this IConstructor value, Action<T> action) where T : IConstructor, new()
        {
            if (action == null)
                throw new ArgumentNullException(Res.ErrArgNull);
            var result = Create<T>(value);
            action(result);
            return result;
        }


    }
}
