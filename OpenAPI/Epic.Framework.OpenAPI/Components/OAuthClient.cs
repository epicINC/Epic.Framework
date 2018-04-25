using Epic.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Extensions;
using System.Collections.Specialized;
using Epic.Extensions;
using System.Net.Http;
using System.Net;

namespace Epic.OpenAPI.Components
{

    public delegate void ReceiveCodeAction(string code, string state, NameValueCollection collection);

    public delegate void SendTokenAction(string code, string state);

    public delegate void ReceiveTokenAction(string access_token, int expires_in, string refresh_token, string openid, string openkey, string state, NameValueCollection collection);


    public class OAuthClient
    {
        /// <summary>
        /// 根据 appSettings 配置项, 创建 OAuthClient
        /// OAuth:Host
        /// OAuth:AppKey
        /// OAuth:AppSecret
        /// </summary>
        /// <returns></returns>
        public static OAuthClient CreateFromAppSetting()
        {
            return CreateFromAppSetting(System.Configuration.ConfigurationManager.AppSettings);
            
        }


        public static OAuthClient CreateFromAppSetting(NameValueCollection colleciton)
        {
            return new OAuthClient(colleciton["OAuth:Host"], colleciton["OAuth:AppKey"], colleciton["OAuth:AppSecret"]);
        }

        public OAuthClient(string host, string appKey, string appSecret, bool isAutoRequest = true)
        {
            if ((this.Host = host).IsNullOrWhiteSpace())
                throw new ArgumentNullException("OAuthClient config Host not found.");

            if ((this.AppKey = appKey).IsNullOrWhiteSpace())
                throw new ArgumentNullException("OAuthClient config AppKey not found.");

            if ((this.AppSecret = appSecret).IsNullOrWhiteSpace())
                throw new ArgumentNullException("OAuthClient config AppSecret not found.");

            this.IsAutoRequest = isAutoRequest;
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

        public bool IsAutoRequest
        {
            get;
            set;
        }



        public string AuthorizeUrl(OAuthResponseType response_type, string client_id, string redirect_uri, string state = null, string scope = null, OAuthDisplayType? display = null)
        {
            if (response_type == OAuthResponseType.Default) throw new ArgumentException("OAuthResponseType: {0} 错误.".Formatting(OAuthResponseType.Default));
            if (client_id.IsNullOrWhiteSpace()) throw new ArgumentException("client_id: 需要填写 appkey.");
            if (redirect_uri.IsNullOrWhiteSpace()) throw new ArgumentException("redirect_uri: 需要填写回调地址.");

            return this.Url("authorize", new { response_type = response_type, client_id = client_id, redirect_uri = redirect_uri, state = state, scope = scope, display = display });

        }

        public string AuthorizeUrl(OAuthResponseType response_type, string redirect_uri, string state = null, string scope = null, OAuthDisplayType? display = null)
        {
            return this.AuthorizeUrl(response_type, this.AppKey, redirect_uri, state, scope, display);
        }

        string TokenUrl(string client_id, string client_secret, string code, string redirect_uri, string state = null)
        {
            if (client_id.IsNullOrWhiteSpace()) throw new ArgumentException("client_id: 需要填写 appkey.");
            if (client_secret.IsNullOrWhiteSpace()) throw new ArgumentException("client_secret: 需要填写 appSecret.");

            return this.Url("token", new { grant_type = OAuthGrantType.authorization_code, client_id = client_id, client_secret = client_secret, code = code, redirect_uri = redirect_uri, state = state });
        }

        string TokenUrl(string code, string redirect_uri, string state = null)
        {
            return this.TokenUrl(this.AppKey, this.AppSecret, code, redirect_uri, state);
        }

        string TokenUrl(string code, string state)
        {
            return this.TokenUrl(code, null, state);
        }


        public void Start()
        {
            this.Start(System.Web.HttpContext.Current);
        }

        public void Start(System.Web.HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException("HttpContext current null.");

            this.Start(context.Request.Params);
        }

        public void Start(NameValueCollection collection)
        {
            if (!collection["code"].IsNullOrWhiteSpace())
                this.OnResponseAuthorize(collection);
            if (!collection["access_token"].IsNullOrWhiteSpace())
                this.OnReceiveToken(collection);
        }

        public void Start(Dictionary<string, string> values)
        {
            this.Start(Convert(values));
        }

        void OnResponseAuthorize(string code, string state, NameValueCollection collection)
        {
            this.OnReceiveCode(code, state, collection);

            if (this.IsAutoRequest)
                this.OnRequestToken(code, state);
        }

        void OnResponseAuthorize(NameValueCollection collection)
        {
            this.OnResponseAuthorize(collection["code"], collection["state"], collection);
        }

        void OnRequestToken(string code, string state)
        {
            var client = new HttpClient();
            var response = client.GetAsync(this.TokenUrl(code, state)).Result;

            Dictionary<string, string> result;

            if (response.IsSuccessStatusCode && (result = response.Content.ReadAsAsync<Dictionary<string, string>>().Result) != null)
                this.OnReceiveToken(Convert(result));
        }

        #region Event

        /// <summary>
        /// 接受 Authorize Code
        /// </summary>
        public event ReceiveCodeAction ReceiveCode;

        void OnReceiveCode(NameValueCollection collection)
        {
            this.OnReceiveCode(collection["code"], collection["state"], collection);
        }

        void OnReceiveCode(string code, string state, NameValueCollection collection)
        {
            if (this.ReceiveCode == null) return;
            this.ReceiveCode(code, state, collection);
        }


        /// <summary>
        /// 请求 Token
        /// </summary>
        public event SendTokenAction SendToken;

        void OnSendToken(string code, string state, NameValueCollection collection)
        {
            if (this.SendToken == null) return;
            this.SendToken(code, state);
        }

        /// <summary>
        /// 接收 Token
        /// </summary>
        public event ReceiveTokenAction ReceiveToken;

        void OnReceiveToken(NameValueCollection collection)
        {
            this.OnReceiveToken(collection["access_token"], collection["expires_in"].AsInt32(), collection["refresh_token"], collection["openid"], collection["openkey"], collection["state"], collection);
        }

        void OnReceiveToken(string access_token, int expires_in, string refresh_token, string openid, string openkey, string state, NameValueCollection collection)
        {
            if (this.ReceiveToken == null) return;
            this.ReceiveToken(access_token, expires_in, refresh_token, openid, openkey, state, collection);
        }

        #endregion

        #region Method

        string Url<T>(string path, T value) where T : class
        {
            var url = new UrlBuilder((this.Host + "/" + path).Replace("///", "/"));
            url.AddQueryString(value);
            return url.ToString();
        }

        static NameValueCollection Convert(Dictionary<string, string> values)
        {
            var result = new NameValueCollection();
            values.ForEach(e => result.Add(e.Key, e.Value));
            return result;
        }

        #endregion

    }
}
