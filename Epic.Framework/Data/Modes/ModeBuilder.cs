using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Data
{
    public class ModeBuilder
    {
        public EntiyMode<T> Entity<T>()
        {
            return null;
        }
    }

    public class EntiyMode<T>
    {
        public void Key<K>(Func<K> keySelector)
        {

        }

        public void Column<C>(Func<C> columnSelector)
        {
        }
    }
}
