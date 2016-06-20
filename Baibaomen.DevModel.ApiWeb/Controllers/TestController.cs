using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Baibaomen.DevModel.ApiWeb.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("hello")]
        public IHttpActionResult GetHello() {
            throw new ApplicationException("test error throw");
            return Ok("Hello Api!");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("Echo")]
        [Authorize(Roles = "admin,system")]
        public IHttpActionResult GetEcho()
        {
            var user = User as ClaimsPrincipal;
            var claims = from c in user.Claims
                         select new
                         {
                             type = c.Type,
                             value = c.Value
                         };

            return Json(claims);
        }
    }
}
