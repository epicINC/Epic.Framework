using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Data.Objects
{
    internal class ObjectQueryExecutionPlan
    {
        ObjectContext context;
        string commandText;
        List<object> parameters;

        internal ObjectQueryExecutionPlan(ObjectContext context, string commandText, List<object> parameters)
        {
            this.context = context;
            this.commandText = commandText;
            this.parameters = parameters;
        }

        internal ObjectResult<T> Execute<T>()
        {
            return new ObjectResult<T>(this.context, this.commandText, this.parameters);
        }

    }
}
