using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Extensions
{

    public interface IExtent<T>
    {
        T Value { get; set; }
    }

    public class Extent<T> : IExtent<T>
    {
        public T Value
        {
            get;
            set;
        }
    }

}
