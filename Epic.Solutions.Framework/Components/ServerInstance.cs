using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Components
{

    public class ServerInstance
    {
        public T Instance<T>() where T : new()
        {
            return ServerInstance<T>.Current;
        }
    }

    public class ServerInstance<T> where T : new()
    {
        static T instance;
        public static T Current
        {
            get
            {
                if (instance != null) return instance;
                return instance = new T();
            }
        }
    }


}
