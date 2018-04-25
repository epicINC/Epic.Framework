using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Extensions
{
    /// <summary>
    /// IEqualityComparer 动态实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal sealed class DynamicEqualityComparer<T> : IEqualityComparer<T>
    {
        Func<T, T, bool> e;
        Func<T, int> g;

        public DynamicEqualityComparer(Func<T, T, bool> equals, Func<T, int> getHashCode = null)
        {
            this.e = equals;
            this.g = getHashCode;
        }

        public bool Equals(T x, T y)
        {
            if (this.e == null)
                return x.Equals(y);
            return this.e(x, y);
        }

        public int GetHashCode(T obj)
        {
            if (this.g == null)
                return obj.GetHashCode();
            return this.g(obj);
        }
    }
}
