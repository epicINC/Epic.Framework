using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.AOP
{
    public interface IAspect
    {
        void BeforeConstructor(object instance, string name, object args);
        object AfterConstructor(object instance, string name);

        void BeforeProperty(object instance, string name, object args);
        object AfterProperty(object instance, string name);

        void BeforeMethod(object instance, string name, object args);
        object AfterMethod(object instance, string name);

    }

    public interface IAspect<T> : IAspect where T : class
    {
        void BeforeConstructor(T instance, string name, object args);
        object AfterConstructor(T instance, string name);

        void BeforeProperty(T instance, string name, object args);
        object AfterProperty(T instance, string name);

        void BeforeMethod(T instance, string name, object args);
        object AfterMethod(T instance, string name);
    }

    internal class AspectBridge<T> : IAspect where T : class
    {
        public AspectBridge(IAspect<T> aspect)
        {
            this.Aspect = aspect;
        }

        IAspect<T> Aspect
        {
            get;
            set;
        }

        public void BeforeConstructor(object instance, string name, object args)
        {
            this.Aspect.BeforeConstructor(instance, name, args);
        }

        public object AfterConstructor(object instance, string name)
        {
            return this.Aspect.AfterConstructor(instance, name);
        }

        public void BeforeProperty(object instance, string name, object args)
        {

        }

        public object AfterProperty(object instance, string name)
        {
            return this.Aspect.AfterProperty(instance, name);
        }

        public void BeforeMethod(object instance, string name, object args)
        {

        }

        public object AfterMethod(object instance, string name)
        {
            return this.Aspect.AfterMethod(instance, name);
        }

    }
}
