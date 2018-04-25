using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Epic.Web
{
    public abstract class ServerContext
    {
        const string prefix = "Epic.ServerContext";

        protected static T Instance<T>() where T : new()
        {
            T config = ContextCache.Get<T>(prefix);
            if (config == null)
            {
                config = new T();
                ContextCache.Set(prefix, config);
            }
            return config;
        }

    }
}
