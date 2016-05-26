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
    public IHttpActionResult Echo()
    {
        var caller = User as ClaimsPrincipal;

        return Json(new
        {
            message = "OK computer",
            client = caller.FindFirst("client_id").Value
        });
    }
}