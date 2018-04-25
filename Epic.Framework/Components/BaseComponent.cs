using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Components
{
    public abstract class BaseComponent : IComponent
    {




        public void Parse(System.Data.SqlClient.SqlDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
