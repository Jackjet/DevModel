using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Baibaomen.FileService.Controllers
{
    /// <summary>
    /// Login to File Server with auth tokens.
    /// </summary>
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        /// <summary>
        /// Login and store token in cookie. You can either login with token stored in Auth header or url param.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public IHttpActionResult LoginWithAuthTokens()
        {
            //Handled by CookieAndUrlTokenProvider in startup configuration. No extra action required.
            return Ok();
        }
    }
}
