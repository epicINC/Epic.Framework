using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Components;

namespace Epic.Data
{
    internal class PagingOrder
    {
        public string Order { get; internal set; }
        public SortDirection Sort { get; internal set; }

        public override string ToString()
        {
            if (this.Sort == SortDirection.Desc)
                return this.Order + " " + SortDirection.Desc;
            return this.Order +" "+ Sort;
        }
    }
}
