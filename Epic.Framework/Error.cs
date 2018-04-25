using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;

namespace Epic
{
    internal static class Error
    {
        [SecuritySafeCritical]
        internal static Exception ArgumentNull(string paramName)
        {
            return new ArgumentNullException(paramName);

        }

        internal static Exception InvalidComplexPropertyExpression(object p0)
        {
            //return new InvalidOperationException(Strings.InvalidComplexPropertyExpression(p0));
            return new InvalidOperationException();
        }

        internal static Exception InvalidPropertiesExpression(object p0)
        {
            return new InvalidOperationException();
        }

    }
}
