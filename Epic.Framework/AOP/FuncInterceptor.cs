using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.AOP
{
    public class FuncInterceptor : IInterceptor
    {
        Func<string, object[], object> beforeCall;
        Action<string, object, object> afterCall;

        Func<string, object, object> beforeAccess;
        Action<string, object, object> afterAccess;


        public FuncInterceptor(Func<string, object[], object> beforeCall, Action<string, object, object> afterCall)
        {
            this.beforeCall = beforeCall;
            this.afterCall = afterCall;
        }

        public FuncInterceptor(Func<string, object, object> beforeAccess, Action<string, object, object> afterAccess)
        {
            this.beforeAccess = beforeAccess;
            this.afterAccess = afterAccess;
        }

        public FuncInterceptor(Func<string, object, object> beforeAccess, Action<string, object, object> afterAccess, Func<string, object[], object> beforeCall, Action<string, object, object> afterCall)
        {
            this.beforeAccess = beforeAccess;
            this.afterAccess = afterAccess;

            this.beforeCall = beforeCall;
            this.afterCall = afterCall;
        }


        public object BeforeCall(string name, object[] values)
        {
            return this.beforeCall != null ? this.beforeCall(name, values) : null;
        }

        public void AfterCall(string name, object result, object state)
        {
            if (this.afterCall != null)
                this.afterCall(name, result, state);
        }

        public object BeforeAccess(string name, object value)
        {
            return this.beforeAccess != null ? this.beforeAccess(name, value) : null;
        }

        public void AfterAccess(string name, object result, object state)
        {
            if (this.afterAccess != null)
                this.afterAccess(name, result, state);
        }
    }

}
