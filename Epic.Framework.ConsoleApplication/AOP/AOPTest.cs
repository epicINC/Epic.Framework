using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.AOP;

namespace Epic.Framework.ConsoleApplication.AOP
{
    public static class AOPTest
    {
        public static void Test()
        {
            var test = ProxyBuilder.Create<TestClass>();
            //InterceptorFactory<TestClass>.RegisterFor(e => e.Title, null, null);
            InterceptorFactory<TestClass>.RegisterFor(e => e.MethodTest(1, 2, ""), null, null);

        }

    }


    public class TestClass
    {

        public int ID
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public DateTime CreateDate
        {
            get;
            set;
        }

        public void MethodTest()
        {
        }

        public void MethodTest(int arg1)
        {
        }

        public void MethodTest(int arg1, int arg2)
        {
        }

        public void MethodTest(int arg1, int arg2, string arg3)
        {
        }


        public int MethodReturn()
        {
            return 0;
        }

        public int MethodReturn(int arg1)
        {
            return arg1++;
        }

        public int MethodReturn(int arg1, int arg2)
        {
            return arg1 + arg2+ 1;
        }

        public int MethodReturn(int arg1, int arg2, string arg3)
        {
            return arg1 + arg2 + 2;
        }


        public string MethodReturnS()
        {
            return "0";
        }

        public string MethodReturnS(int arg1)
        {
            return ""+ arg1++;
        }

        public string MethodReturnS(int arg1, int arg2)
        {
            return "" + arg1 + arg2 + 1;
        }

        public string MethodReturnS(int arg1, int arg2, string arg3)
        {
            return "" + arg1 + arg2 + 2;
        }

    }


    public class TestClass_Proxy : TestClass
    {
        IInterceptor inspector;
        TestClass_Proxy()
        {
            this.inspector = InterceptorFactory<TestClass>.Create();
        }

        public int MethodReturn()
        {
            object state = this.inspector.BeforeCall("MethodTest", null);
            var result = base.MethodReturn();
            this.inspector.AfterCall("MethodTest", result, state);
            return result;
        }

    }

}
