using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Converter;
using System.Data;

namespace Epic.Extensions
{
    public static class TypeExtensions
    {
        public static DbType AsDbType(this Type value)
        {
            return DbTypeConverter.AsDbType(value);
        }
    }
}
