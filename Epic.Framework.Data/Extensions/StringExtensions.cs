using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Converter;

namespace Epic.Extensions
{
    public static class StringExtensions
    {
        public static DbType AsDbType(this string value)
        {
            return SqlDbTypeConverter.AsDbType(value);
        }
    }
}
