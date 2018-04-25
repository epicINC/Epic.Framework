using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Epic.Data.Mapper;

namespace Epic.Data.SqlMapper
{
    internal class SqlDbDefinition : DbTypeDefinition
    {

        internal SqlDbDefinition()
        {
            this.Command = typeof(SqlCommand);
            this.ParameterCollection = typeof(SqlParameterCollection);
            this.Parameter = typeof(SqlParameter);

            this.Reader = typeof(System.Data.Common.DbDataReader);

            this.InitCommand();
            this.InitReader();

        }

        


    }
}
