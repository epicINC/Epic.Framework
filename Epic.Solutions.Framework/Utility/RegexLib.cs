using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Utility
{
    public static class RegexLib
    {
        /// <summary>
        /// 手机号码正则表达式
        /// 中国移动 139|138|137|136|135|134|159|150|151|158|157|188|187|152|182|147
        /// 中国联通 130|131|132|155|156|185|186|145
        /// 中国电信 133|153|180|181|189
        /// </summary>
        public const string ChinaMobile = @"^(139|138|137|136|135|134|159|150|151|158|157|188|187|152|182|147|130|131|132|155|156|185|186|145|133|153|180|181|189)\d{8}$";


    }
}
