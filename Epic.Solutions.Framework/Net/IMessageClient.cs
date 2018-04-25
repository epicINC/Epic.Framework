using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Net
{
    public interface IMessageClient
    {
        string Name { get; set; }
        string Address { get; set; }

        void Send(string value);
    }
}
