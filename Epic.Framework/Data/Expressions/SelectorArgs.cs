using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Data.Expressions
{
    public class SelectorArgs
    {
        internal SelectorArgs(ConditionBuilder builder)
        {
            this.Commond = builder.Condition;
            this.Args = builder.Arguments;
            this.Filter = builder.Filter;
        }

        public string Filter
        {
            get;
            private set;
        }


        public string Commond
        {
            get;
            private set;
        }

        public object[] Args
        {
            get;
            private set;
        }
    }
}
