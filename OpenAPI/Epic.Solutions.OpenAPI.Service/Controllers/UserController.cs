using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Epic.Solutions.OpenAPI.Service.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="oauth_consumer_key">appid</param>
        /// <param name="openid">用户 ID</param>
        /// <returns></returns>
        [Route("get_user_info")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult UserInfo(string access_token, string oauth_consumer_key, string openid)
        {
            return null;
        }
    }
}