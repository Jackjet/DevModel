using IdentityModel.Client;
using System;
using System.Web;
using System.Web.Http;

namespace Baibaomen.DevModel.CA.Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        static readonly string CAAddress = System.Configuration.ConfigurationManager.AppSettings["CA.Address"];
        static readonly string CAUserId = System.Configuration.ConfigurationManager.AppSettings["CA.User.Id"];
        static readonly string CAUserSecret = System.Configuration.ConfigurationManager.AppSettings["CA.User.Secret"];
        static string CAUserScopes = System.Configuration.ConfigurationManager.AppSettings["CA.User.Scopes"];
        static readonly string CASystemId = System.Configuration.ConfigurationManager.AppSettings["CA.System.Id"];
        static readonly string CASystemSecret = System.Configuration.ConfigurationManager.AppSettings["CA.System.Secret"];
        static string CASystemScopes = System.Configuration.ConfigurationManager.AppSettings["CA.System.Scopes"];

        [Route("hello")]
        [HttpGet]
        public IHttpActionResult Hello() {
            return Ok("Hello api");
        }

        /// <summary>
        /// Login and get access token.
        /// </summary>
        /// <param name="loginView"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public IHttpActionResult Login(LoginView loginView)
        {

            var resp = GetUserToken(loginView.Account, loginView.Password);
            return Ok(new { AccessToken = resp.AccessToken });
        }

        //[Route("logout")]
        //[HttpPost]
        //public IHttpActionResult Logout() {

        //}

        TokenResponse GetUserToken(string account, string password)
        {
            var client = new TokenClient(
                HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + CAAddress + "/connect/token",
                CAUserId,
                CAUserSecret);

            return client.RequestResourceOwnerPasswordAsync(account, password, CAUserScopes).Result;
        }
    }
}
