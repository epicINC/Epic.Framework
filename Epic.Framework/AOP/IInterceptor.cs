using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.AOP
{
    public interface IInterceptor : IMethodInterceptor, IPropertyInterceptor
    {



    }

    public interface IMethodInterceptor
    {
        object BeforeCall(string name, object[] values);
        void AfterCall(string name, object result, object state);
    }

    public interface IPropertyInterceptor
    {
        object BeforeAccess(string name, object value);
        void AfterAccess(string name, object result, object state);
    }

}
