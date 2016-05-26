using Owin;
using System.Web.Http;
using Baibaomen.DevModel.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ExceptionHandling;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using Baibaomen.DevModel.ApiSite;
using log4net;
using Microsoft.Owin.Cors;

public class WebApiConfig
{
    public static void Configure(IAppBuilder app)
    {
        HttpConfiguration config = new HttpConfiguration();
        
        config.MapHttpAttributeRoutes();

        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        config.Services.Add(typeof(IExceptionLogger), new HttpExceptionLogger(e=>logger.Error("Unhandled exception occurred",e)));

        config.Services.Replace(typeof(IExceptionHandler), new UnhandledExceptionHandler());

        var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
        jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

        app.UseCors(CorsOptions.AllowAll);

        config.EnableETag(new List<string>() { "/api/"}, ExecutionMode.Include);

        app.UseWebApi(config);
    }
}