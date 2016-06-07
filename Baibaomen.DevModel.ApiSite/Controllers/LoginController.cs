using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Baibaomen.DevModel.ApiSite.Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController:ApiController
    {
        static readonly string CAAddress = System.Configuration.ConfigurationManager.AppSettings["CA.Address"];
        static readonly string CAUserId = System.Configuration.ConfigurationManager.AppSettings["CA.User.Id"];
        static readonly string CAUserSecret = System.Configuration.ConfigurationManager.AppSettings["CA.User.Secret"];
        static readonly string CAUserScopes = System.Configuration.ConfigurationManager.AppSettings["CA.User.Scopes"];
        static readonly string CASystemId = System.Configuration.ConfigurationManager.AppSettings["CA.System.Id"];
        static readonly string CASystemSecret = System.Configuration.ConfigurationManager.AppSettings["CA.System.Secret"];
        static readonly string CASystemScopes = System.Configuration.ConfigurationManager.AppSettings["CA.System.Scopes"];

        /// <summary>
        /// Login and get access token.
        /// </summary>
        /// <param name="loginView"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public IHttpActionResult Login(LoginView loginView) {

            var resp = GetUserToken(loginView.Account,loginView.Password);
            return Ok(new { AccessToken = resp.AccessToken});
        }

        //[Route("logout")]
        //[HttpPost]
        //public IHttpActionResult Logout() {

        //}
        
        static TokenResponse GetUserToken(string account, string password)
        {
            var client = new TokenClient(
                CAAddress + "/connect/token",
                CAUserId,
                CAUserSecret);

            return client.RequestResourceOwnerPasswordAsync(account,password, CAUserScopes).Result;
        }
    }
}