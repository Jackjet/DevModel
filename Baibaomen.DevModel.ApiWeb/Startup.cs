using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using AutoMapper;
using Baibaomen.DevModel.ApiWeb;
using Baibaomen.DevModel.Business.AutoMapper;
using Baibaomen.DevModel.Infrastructure;
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

        ConfigureSwagger(config);

        ConfigureJson(app, config);

        ConfigureWebApi(app, config);

        ConfigureAutofac(app, config);

        ConfigureAutoMapper();

        ConfigureIdentityServer(app);

        app.UseWebApi(config);
    }

    private void ConfigureIdentityServer(IAppBuilder app)
    {
        JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

        app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
        {
            Authority = authority,
            RequiredScopes = "api".Split(' ')
        });

        //// add app local claims per request
        //app.UseClaimsTransformation(incoming =>
        //{
        //    // either add claims to incoming, or create new principal
        //    var appPrincipal = new ClaimsPrincipal(incoming);
        //    incoming.Identities.First().AddClaim(new Claim("appSpecific", "some_value"));

        //    return Task.FromResult(appPrincipal);
        //});
    }

    private void ConfigureAutofac(IAppBuilder app, HttpConfiguration config)
    {
        var builder = new ContainerBuilder();
        builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
        builder.RegisterModule(new AutofacModule());
        builder.RegisterModule(new Baibaomen.DevModel.Businsess.AutofacModule());

        var container = builder.Build();
        config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        app.UseAutofacMiddleware(container);
        app.UseAutofacWebApi(config);
    }

    private void ConfigureAutoMapper()
    {
        Mapper.Initialize(cfg =>
        {
            cfg.AddProfile<ViewModelsProfile>();
        });
    }

    private void ConfigureJson(IAppBuilder app, HttpConfiguration config)
    {
        var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
        jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        jsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
        jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    }

    private void ConfigureSwagger(HttpConfiguration config)
    {
        config.EnableSwagger(c =>
        {
            c.SingleApiVersion("v1", "WebAPI");
            c.IncludeXmlComments(GetXmlCommentsPath());

            c.OAuth2("oauth2")
              .Description("api")
              .Flow("implicit")
              .AuthorizationUrl(authority + "connect/authorize")
              .Scopes(scopes =>
              {
                  scopes.Add("api", "");
              });

            //Apply oauth filter.
            c.OperationFilter<OAuth2OperationFilter>();

        }).EnableSwaggerUi(c =>
            c.EnableOAuth2Support("api_doc", "238B3DFC-5AC1-42D7-A705-2B6A3356EFC9", "test_realm", "Baibaomen Api")
        );
    }

    private static string GetXmlCommentsPath()
    {
        return String.Format(@"{0}\bin\Baibaomen.DevModel.ApiWeb.XML",
                AppDomain.CurrentDomain.BaseDirectory);
    }

    private void ConfigureWebApi(IAppBuilder app, HttpConfiguration config)
    {
        config.MapHttpAttributeRoutes();

        config.AddModelValidationFilter();

        app.UseCors(CorsOptions.AllowAll);

        config.EnableETag(new List<string>() { "/api/" }, ExecutionMode.Include);
    }

    private void ConfigureExceptionAndLog(IAppBuilder app, HttpConfiguration config)
    {
        ILog logger = LogManager.GetLogger("logger");

        config.Services.Replace(typeof(IExceptionHandler), new UnhandledExceptionHandler(x => logger.Error($"Unhandled exception occurred\nURL:\n{x.Request.RequestUri}\nHeaders:\n{x.Request.Headers}", x.Exception)));
    }
}