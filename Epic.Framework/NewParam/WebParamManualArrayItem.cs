using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.NewParam
{
    /// <summary>
    /// 自定义参数数组对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="K"></typeparam>
    public class WebParamManualArrayItem<T, K> : WebParamArrayItem<T, K> where T:new()
    {

        internal WebParamManualArrayItem(WebParam<T> parent, string name)
        {
            this.Parent = parent;
            this.Name = name;
        }

        K[] value;
        public override K[] Value
        {
            get
            {
                this.Raise();
                return this.value;
            }
            internal set { this.value = value; }
        }
        
    }
}
