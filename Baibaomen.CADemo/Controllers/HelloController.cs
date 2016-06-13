using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Baibaomen.CADemo.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok("Hello Web Api");
        }
    }
}
