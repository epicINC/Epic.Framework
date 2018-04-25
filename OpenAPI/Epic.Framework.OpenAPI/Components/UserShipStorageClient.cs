using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Epic.OpenAPI.Components
{
 

    public class UserShipStorageClient
    {
        public UserShipStorageClient(string appKey, string appSecret)
        {
            this.AppKey = appKey;
            this.AppSecret = appSecret;
        }

        public string Host
        {
            get;
            set;
        }

        public string AppKey
        {
            get;
            set;
        }

        public string AppSecret
        {
            get;
            set;
        }

        #region Find

        public string Find(string appKey, string appSecret, string openid, string access_token, string id)
        {
            var client = new HttpClient();

            return null;
        }

        public string Find(string openid, string access_token, string id)
        {
            return this.Find(this.AppKey, this.AppSecret, openid, access_token, id);
        }

        public string FindByOpenID(string appKey, string appSecret, string openid)
        {
            return this.Find(appKey, appSecret, openid, null, null);
        }

        public string FindByOpenID(string openid)
        {
            return this.Find(openid, null, null);
        }

        public string FindByAccessToken(string appKey, string appSecret, string access_token)
        {
            return this.Find(appKey, appSecret, null, access_token, null);
        }

        public string FindByAccessToken(string access_token)
        {
            return this.Find(null, access_token, null);
        }

        public string FindByID(string appKey, string appSecret, string id)
        {
            return this.Find(appKey, appSecret, null, null, id);
        }

        public string FindByID(string id)
        {
            return this.Find(null, null, id);
        }

        #endregion

    }
}
