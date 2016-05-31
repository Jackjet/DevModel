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
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Baibaomen.DevModel.ApiSite
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigExceptionAndLog(app, config);

            ConfigWebApi(app, config);

            ConfigJson(app, config);

            ConfigIdentityServer(app);

            ConfigSwagger(config);

            app.UseWebApi(config);
        }

        private void ConfigSwagger(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "WebAPI");
                c.IncludeXmlComments(GetXmlCommentsPath());
            }).EnableSwaggerUi();

        }

        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\Baibaomen.DevModel.ApiSite.XML",
                    System.AppDomain.CurrentDomain.BaseDirectory);
        }

        private void ConfigWebApi(IAppBuilder app, HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            app.UseCors(CorsOptions.AllowAll);

            config.EnableETag(new List<string>() { "/api/" }, ExecutionMode.Include);
        }

        private void ConfigIdentityServer(IAppBuilder app)
        {
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = ConfigurationManager.AppSettings["CAAddress"],
                ValidationMode = ValidationMode.ValidationEndpoint,
                RequiredScopes = new[] { "api1" },
                 
            });
        }

        private void ConfigExceptionAndLog(IAppBuilder app, HttpConfiguration config)
        {
            ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            config.Services.Add(typeof(IExceptionLogger), new HttpExceptionLogger(e => 
                logger.Error("Unhandled exception occurred", e)
            ));

            config.Services.Replace(typeof(IExceptionHandler), new UnhandledExceptionHandler());
        }

        private void ConfigJson(IAppBuilder app,HttpConfiguration config) {
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    }
}