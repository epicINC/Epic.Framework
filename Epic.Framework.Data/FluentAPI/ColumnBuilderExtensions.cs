using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Data
{
    public static class ColumnBuilderExtensions
    {
        public static ColumnBuilder<T, string> MaxLength<T>(this ColumnBuilder<T, string> value, int? length)
        {
            return value;
        }

        public static ColumnBuilder<T, string> IsFixedLength<T>(this ColumnBuilder<T, string> value)
        {
            return value;
        }

        public static ColumnBuilder<T, string> IsVariableLength<T>(this ColumnBuilder<T, string> value)
        {
            return value;
        }

        public static ColumnBuilder<T, string> IsUnicode<T>(this ColumnBuilder<T, string> value)
        {
            return value.IsUnicode(true);
        }

        public static ColumnBuilder<T, string> IsUnicode<T>(this ColumnBuilder<T, string> value, bool unicode)
        {
            return value;
        }
    }
}
