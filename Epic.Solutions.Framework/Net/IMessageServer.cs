using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Net
{
    public interface IMessageServer
    {
        string Name { get; set; }
        void Start();
        void Stop();
    }
}
