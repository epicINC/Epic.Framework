using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components
{
    /// <summary>
    /// 垂直方向数据结构(左, 右)
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class TupleVertical<T1, T2>
    {
        public TupleVertical()
        {

        }

        public TupleVertical(T1 top, T2 bottom)
        {
            this.Top = top;
            this.Bottom = bottom;
        }

        public T1 Top
        {
            get;
            set;
        }

        public T2 Bottom
        {
            get;
            set;
        }
    }
}
