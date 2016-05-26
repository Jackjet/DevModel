using System.Web.Http;

[RoutePrefix("api/Test")]
public class TestController : ApiController
{
    [Route("")]
    public IHttpActionResult Get()
    {
        return Ok("Hello webapi");
    }
}