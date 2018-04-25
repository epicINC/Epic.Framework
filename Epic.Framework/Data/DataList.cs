using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Data
{
    public class DataList<T> : List<T>
    {

        public Paging<T> Paging
        {
            get;
            set;
        }
    }
}
