using Owin;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Routing;

public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        ConfigureWebApi(app);
    }
    
    public static void ConfigureWebApi(IAppBuilder app)
    {
        HttpConfiguration config = new HttpConfiguration();

        // Web API routes
        config.MapHttpAttributeRoutes();

        // Json formatter
        config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

        app.UseWebApi(config);
    }
}