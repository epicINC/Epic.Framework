using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Components
{

    /// <summary>
    /// 水平方向数据结构(左, 右)
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class TupleHorizontal<T1, T2>
    {
        public TupleHorizontal()
        {

        }

        public TupleHorizontal(T1 left, T2 right)
        {

        }

        public T1 Left
        {
            get;
            set;
        }

        public T2 Right
        {
            get;
            set;
        }
    }
}
