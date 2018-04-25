using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Epic.Extensions;

namespace Epic
{
    public class Errors
    {

        public static Exception CheckArgument(bool condition, string paramName, string message)
        {
            return condition ? new ArgumentException(message, paramName) : null;
        }

        public static Exception Argument(string message, string paramName)
        {
            return new ArgumentException(message, paramName);
        }

        #region ArgumentNull

        [SecuritySafeCritical]
        public static Exception ArgumentNull(string paramName, string message = null)
        {
            return new ArgumentNullException(paramName, message);
        }

        [SecuritySafeCritical]
        public static Exception CheckArgumentNull(bool condition, string paramName, string message = null)
        {
            return condition ? ArgumentNull(paramName, message) : null;
        }



        [SecuritySafeCritical]
        public static Exception CheckArgumentNull(Func<bool> action, string paramName, string message = null)
        {
            if (action != null)
                return CheckArgumentNull(action(), paramName, message);
            return ArgumentNull(paramName, message);
        }

        [SecuritySafeCritical]
        public static Exception ArgumentNull(string paramName, Func<string> message = null)
        {
            return ArgumentNull(paramName, message.Func(e => 
                {
                    if (message.IsNull()) return null;
                    return message();
                }));
        }

        [SecuritySafeCritical]
        public static Exception CheckArgumentNull(bool condition, string paramName, Func<string> message = null)
        {
            return condition ? ArgumentNull(paramName, message) : null;
        }

        [SecuritySafeCritical]
        public static Exception CheckArgumentNull(Func<bool> action, string paramName, Func<string> message = null)
        {
            if (action != null)
                return CheckArgumentNull(action(), paramName, message);
            return ArgumentNull(paramName, message);
        }


        [SecuritySafeCritical]
        public static Exception CheckArgumentNull<T>(T value, string paramName, string message = null)
        {
            return CheckArgumentNull(value.IsNull(), paramName, message);
        }

        [SecuritySafeCritical]
        public static Exception CheckArgumentNull<T>(T value, string paramName, Func<string> message)
        {
            return CheckArgumentNull(value.IsNull(), paramName, message);
        }

        #endregion


        public static Exception InvalidComplexPropertyExpression(object p0)
        {
            //return new InvalidOperationException(Strings.InvalidComplexPropertyExpression(p0));
            return new InvalidOperationException();
        }

        public static Exception InvalidPropertiesExpression(object p0)
        {
            return new InvalidOperationException();
        }

    }


    public static class ErrorExtensions
    {
        public static void Throw(this Exception value)
        {
            if (value != null) throw value;
        }
    }

}

