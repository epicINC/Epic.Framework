using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Epic.NewParam
{

    public class RuleForExpression<T, K> where T : new()
    {
        public RuleForExpression(BaseWebParamItem<T, K> item)
        {
            this.Parent = item.Parent;
            this.Item = item;
        }

        internal WebParam<T> Parent
        {
            get;
            set;
        }

        internal BaseWebParamItem<T, K> Item
        {
            get;
            set;
        }


    }


}
