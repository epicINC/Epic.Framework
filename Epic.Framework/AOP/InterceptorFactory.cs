using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Epic.FluentAPI;
using System.Reflection;

namespace Epic.AOP
{
    public static class InterceptorFactory<T>
    {
        static ObjectInterceptor context;
        static Type ObjectType;

        static InterceptorFactory()
        {
            context = new ObjectInterceptor();
            ObjectType = typeof(T);
        }


        public static IInterceptor Create()
        {
            return null;
        }


        public static void Register(IInterceptor value)
        {
            context.Property.Add(value);
            context.Method.Add(value);
        }


        public static void Register(Func<string, object[], object> beforeCall, Action<string, object, object> afterCall)
        {
            Register(new FuncInterceptor(beforeCall, afterCall));
        }

        public static void RegisterFor<K>(Expression<Func<T, K>> selector, IInterceptor value)
        {
            RegisterFor(Epic.FluentAPI.SimpleAccess.Find(selector), value);
        }

        public static void RegisterFor(Expression<Action<T>> selector, Func<string, object[], object> beforeCall, Action<string, object, object> afterCall)
        {
            var member = Epic.FluentAPI.SimpleAccess.Find(selector);
            //member.GetParameters();
            RegisterFor(member, new FuncInterceptor(beforeCall, afterCall));
        }

        static void RegisterFor(MemberInfo member, IInterceptor value)
        {
            context.Set(member.Name, value);
        }

        public static void RegisterForProperty(Func<string, object[], object> beforeCall, Action<string, object, object> afterCall)
        {
            RegisterForProperty(new FuncInterceptor(beforeCall, afterCall));
        }

        public static void RegisterForProperty(IInterceptor value)
        {
            context.Property.Add(value);
        }


        public static void RegisterForMethod(Func<string, object[], object> beforeCall, Action<string, object, object> afterCall)
        {
            RegisterForMethod(new FuncInterceptor(beforeCall, afterCall));
        }

        public static void RegisterForMethod(IInterceptor value)
        {
            context.Method.Add(value);
        }

    }
}
