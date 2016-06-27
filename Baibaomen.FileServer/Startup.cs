using Baibaomen.DevModel.Infrastructure;
using Baibaomen.FileService;
using IdentityServer3.AccessTokenValidation;
using log4net;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

/// <summary>
/// 
/// </summary>
public class Startup
{
    string authority = ConfigurationManager.AppSettings["CA.Address"];

    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    public void Configuration(IAppBuilder app)
    {
        HttpConfiguration config = new HttpConfiguration();

        ConfigureExceptionAndLog(app, config);

        ConfigureWebApi(app, config);

        ConfigureJson(app, config);

        ConfigureIdentityServer(app);

        ConfigureSwagger(config);

        app.UseWebApi(config);
    }

    void ConfigureSwagger(HttpConfiguration config)
    {
        config.EnableSwagger(c =>
        {
            c.SingleApiVersion("v1", "File-Service");
            c.IncludeXmlComments(GetXmlCommentsPath());

            c.OAuth2("oauth2")
              .Description("api")
              .Flow("implicit")
              .AuthorizationUrl(authority + "connect/authorize")
              .Scopes(scopes =>
              {
                  scopes.Add("file-service", "");
              });

            //Apply oauth filter.
            c.OperationFilter<OAuth2OperationFilter>();

        }).EnableSwaggerUi(c =>
            c.EnableOAuth2Support("file_service_doc", "8D539EC6-7E6B-41F5-BD4D-95CF50511796", "test_realm", "Baibaomen File Service")
        );
    }

    private static string GetXmlCommentsPath()
    {
        return String.Format(@"{0}\bin\Baibaomen.FileService.XML",
                AppDomain.CurrentDomain.BaseDirectory);
    }

    void ConfigureIdentityServer(IAppBuilder app)
    {
        JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

        app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
        {
            Authority = authority,
            RequiredScopes = "file-service".Split(' '),
            TokenProvider = new CookieAndUrlTokenProvider("token")
        });
    }

    void ConfigureWebApi(IAppBuilder app, HttpConfiguration config)
    {
        config.MapHttpAttributeRoutes();

        app.UseCors(CorsOptions.AllowAll);

        config.EnableETag(new List<string>() { "/api/" }, ExecutionMode.Include);
    }

    private void ConfigureJson(IAppBuilder app, HttpConfiguration config)
    {
        var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
        jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    }

    private void ConfigureExceptionAndLog(IAppBuilder app, HttpConfiguration config)
    {
        ILog logger = LogManager.GetLogger("logger");
        config.Services.Add(typeof(IExceptionLogger), new HttpExceptionLogger(e =>
        {
            var headers = new List<string>();
            for (int i = 0; i < HttpContext.Current.Request.Headers.Count; i++)
            {
                headers.Add(HttpContext.Current.Request.Headers.AllKeys[i] + ":" + HttpContext.Current.Request.Headers[i]);
            }

            logger.Error("Unhandled exception occurred\nURL:\n" + HttpContext.Current.Request.Url + "\nHeaders:\n" + string.Join("\n", headers), e);
        }
        ));

        config.Services.Replace(typeof(IExceptionHandler), new UnhandledExceptionHandler());
    }
}