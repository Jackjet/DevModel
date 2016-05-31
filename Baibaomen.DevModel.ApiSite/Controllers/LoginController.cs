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
        
        static TokenResponse GetUserToken(string account, string password)
        {
            var client = new TokenClient(
                "https://localhost:44301/identity/connect/token",
                "carbon",
                "21B5F798-BE55-42BC-8AA8-0025B903DC3B");

            return client.RequestResourceOwnerPasswordAsync(account,password, "api1").Result;
        }
    }
}