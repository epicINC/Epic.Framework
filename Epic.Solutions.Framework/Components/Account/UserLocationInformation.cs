using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components
{
    /// <summary>
    /// 用户地址信息
    /// </summary>
    public class UserLocationInformation
    {
        public string Country 
        {
            get;
            set;
        }

        public string Province
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }

        public string Zone
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string Zip
        {
            get;
            set;
        }
    }
}
