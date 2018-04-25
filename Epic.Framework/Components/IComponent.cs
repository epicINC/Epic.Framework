using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Epic.Components
{
    public interface IComponent
    {
        void Parse(SqlDataReader reader);
    }
}
