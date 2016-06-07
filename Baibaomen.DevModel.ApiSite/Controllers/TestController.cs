using System.Linq;
using System.Security.Claims;
using System.Web.Http;

[RoutePrefix("api/Test")]
public class TestController : ApiController
{
    [Route("")]
    public IHttpActionResult Get()
    {
        return Ok("Hello webapi");
    }

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