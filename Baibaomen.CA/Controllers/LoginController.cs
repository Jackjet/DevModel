using Baibaomen.CA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Baibaomen.CA.Controllers
{
    /// <summary>
    /// Handle CA logins.
    /// </summary>
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Login(LoginViewModel login) {
            throw new ApplicationException("hello exception");

            return Ok("Hello api");
        }
    }
}
