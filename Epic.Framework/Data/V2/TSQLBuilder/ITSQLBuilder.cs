using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Data.V2.TSQLBuilder
{
    interface ITSQLBuilder
    {
        QueryType QueryType { get; }
    }
}
