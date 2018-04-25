using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Data
{
    public static class ModelBuilder
    {
        public static TableBuilder<T> Entity<T>()
        {
            return new TableBuilder<T>();
        }
    }
}
