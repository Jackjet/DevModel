﻿using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using AutoMapper;
using Baibaomen.DevModel.ApiSite.AutoMapper;
using Baibaomen.DevModel.Businsess;
using Baibaomen.DevModel.Infrastructure;
using IdentityServer3.AccessTokenValidation;
using log4net;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
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

            ConfigureExceptionAndLog(app, config);

            ConfigureWebApi(app, config);

            ConfigureJson(app, config);

            //ConfigIdentityServer(app);

            ConfigureSwagger(config);

            ConfigureAutoMapper();

            ConfigureAutofac(app, config);

            app.UseWebApi(config);
        }
        
        private void ConfigureAutofac(IAppBuilder app,HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterModule(new AutofacModule());
            builder.RegisterModule(new Businsess.AutofacModule());

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
        }

        private void ConfigureAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<PropertyMapperProfile>();
            });
        }

        private void ConfigureSwagger(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "WebAPI");
                c.IncludeXmlComments(GetXmlCommentsPath());
            }).EnableSwaggerUi();

        }

        private static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\Baibaomen.DevModel.ApiSite.XML",
                    System.AppDomain.CurrentDomain.BaseDirectory);
        }

        private void ConfigureWebApi(IAppBuilder app, HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            app.UseCors(CorsOptions.AllowAll);

            config.EnableETag(new List<string>() { "/api/" }, ExecutionMode.Include);
        }

        private void ConfigIdentityServer(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            string authority = ConfigurationManager.AppSettings["CA.Address"];
            
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = authority,
                RequiredScopes = new[] { "api" }
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

        private void ConfigureExceptionAndLog(IAppBuilder app, HttpConfiguration config)
        {
            ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            config.Services.Add(typeof(IExceptionLogger), new HttpExceptionLogger(e => 
                logger.Error("Unhandled exception occurred", e)
            ));

            config.Services.Replace(typeof(IExceptionHandler), new UnhandledExceptionHandler());
        }

        private void ConfigureJson(IAppBuilder app,HttpConfiguration config) {
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    }
}