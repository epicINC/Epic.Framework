using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Emit
{
    public interface IGenericConstructor<T> : IConstructor
    {
        T Builder { get; set; }
    }

    public abstract class BaseBuilderConstructor<T> : BaseConstructor, IGenericConstructor<T>
    {
        public T Builder
        {
            get;
            set;
        }
    }
}
