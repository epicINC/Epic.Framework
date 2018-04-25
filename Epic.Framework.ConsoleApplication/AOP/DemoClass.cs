using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Framework.ConsoleApplication.AOP
{
    public class DemoInterceptor : Epic.AOP.IInterceptor
    {
        public object BeforeCall(string name, object[] values)
        {
            throw new NotImplementedException();
        }

        public void AfterCall(string name, object result, object state)
        {
            throw new NotImplementedException();
        }

        public object BeforeAccess(string name, object value)
        {
            throw new NotImplementedException();
        }

        public void AfterAccess(string name, object result, object state)
        {
            throw new NotImplementedException();
        }
    }

    public class DemoClass
    {
        public int ID
        {
            get;
            set;
        }

        public void TestMethod1()
        {
        }

        public int TestMethod2(int a)
        {
            return a + 1;
        }
    }

    public class DemoClass2 : DemoClass
    {
        Epic.AOP.IInterceptor interceptor = new DemoInterceptor();
        DemoClass2()
        {
        }


        public int ID
        {
            get
            {
                var state = interceptor.BeforeCall("ID", null);
                var result = base.ID;
                interceptor.AfterCall("ID", result, state);
                return result;
            }
            set
            {

                var state = interceptor.BeforeCall("ID", new object[] { value });
                var result = base.ID = value;
                interceptor.AfterCall("ID", result, state);
            }
        }


        public void TestMethod1()
        {
            var state = interceptor.BeforeCall("TestMethod1", null);
            base.TestMethod1();
            interceptor.AfterCall("TestMethod1", null, state);
        }

        public int TestMethod2(int a)
        {
            var state = interceptor.BeforeCall("TestMethod1", new object[] { a });
            var result = base.TestMethod2(a);
            interceptor.AfterCall("TestMethod1", result, state);
            return result;
        }
    }
}
