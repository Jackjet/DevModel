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
    [Authorize]
    public IHttpActionResult GetEcho()
    {
        var caller = User as ClaimsPrincipal;

        var subjectClaim = caller.FindFirst("sub");
        if (subjectClaim != null)
        {
            return Json(new
            {
                message = "OK user",
                client = caller.FindFirst("client_id").Value,
                subject = subjectClaim.Value
            });
        }
        else
        {
            return Json(new
            {
                message = "OK computer",
                client = caller.FindFirst("client_id").Value
            });
        }
    }
}