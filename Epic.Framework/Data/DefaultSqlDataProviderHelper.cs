using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Epic.Data
{
    public static class DefaultSqlDataProviderHelper
    {
        public static SqlParameterCollection AddRange(this SqlParameterCollection param, ParameterList list)
        {
            if (list != null && list.Count > 0)
                list.ForEach(e => param.Add(e));
            return param;
        }
    }
}
