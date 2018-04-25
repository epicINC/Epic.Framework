using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Epic.Mapper;

namespace Epic.Data.Mapper
{
    public interface IDataMapper<Source, Dest> : IObjectMapper<Source, Dest>
        where Source : System.Data.IDataReader
        where Dest : class
    {
        int Insert(IDbCommand command, Dest value);
        int Update(IDbCommand command, Dest value);
        int Delete(IDbCommand command, Dest value);
    }
}
