/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Owin;
using Baibaomen.CA.IdSvr;
using IdentityManager;
using IdentityManager.Configuration;
using IdentityServer3.Core.Configuration;
using Baibaomen.CA.IdMgr;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Twitter;
using IdentityManager.Core.Logging;
using Serilog;
using IdentityManager.Logging;
using System.Web.Http;
using log4net;
using System.Web.Http.ExceptionHandling;
using Baibaomen.DevModel.Infrastructure;
using System.Web;
using System.Net.Http.Headers;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.Owin.Cors;
using Swashbuckle.Application;
using System.Collections.Generic;

namespace Baibaomen.CA
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureExceptionAndLog(app, config);

            ConfigureWebApi(app, config);

            ConfigureSwagger(config);

            ConfigureIdentityServer(app);
        }


        void ConfigureExceptionAndLog(IAppBuilder app, HttpConfiguration config)
        {
            log4net.ILog logger = LogManager.GetLogger("logger");
            config.Services.Add(typeof(IExceptionLogger), new HttpExceptionLogger(e =>
                {
                    var headers = new List<string>();
                    for (int i = 0; i < HttpContext.Current.Request.Headers.Count; i++)
                    {
                        headers.Add(HttpContext.Current.Request.Headers.AllKeys[i] + ":" + HttpContext.Current.Request.Headers[i]);
                    }

                    logger.Error("Unhandled exception occurred\nURL:\n" + HttpContext.Current.Request.Url + "\nHeaders:\n" + string.Join("\n",headers), e);
                }
            ));

            config.Services.Replace(typeof(IExceptionHandler), new UnhandledExceptionHandler());
        }
        
        void ConfigureWebApi(IAppBuilder app, HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            // Json formatter
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            app.UseCors(CorsOptions.AllowAll);

            app.UseWebApi(config);
        }

        void ConfigureSwagger(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Baibaomen.CA");
                c.IncludeXmlComments(GetXmlCommentsPath());
                c.UseFullTypeNameInSchemaIds();
            }).EnableSwaggerUi(c => { c.DocExpansion(DocExpansion.List); });

        }

        string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\Baibaomen.CA.XML",
                    System.AppDomain.CurrentDomain.BaseDirectory);
        }

        public void ConfigureIdentityServer(IAppBuilder app)
        {
            LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider());
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Trace()
               .CreateLogger();

            app.Map("/admin", adminApp =>
            {
                var factory = new IdentityManagerServiceFactory();
                factory.ConfigureSimpleIdentityManagerService("CADB");
                //factory.ConfigureCustomIdentityManagerServiceWithIntKeys("AspId_CustomPK");

                adminApp.UseIdentityManager(new IdentityManagerOptions()
                {
                    Factory = factory
                });
            });

            app.Map("/core", core =>
            {
                var idSvrFactory = Factory.Configure();
                idSvrFactory.ConfigureUserService("AspId");
                //idSvrFactory.ConfigureCustomUserService("AspId_CustomPK");

                var options = new IdentityServerOptions
                {
                    SiteName = "IdentityServer3 - UserService-AspNetIdentity",
                    SigningCertificate = Certificate.Get(),
                    Factory = idSvrFactory,
                    AuthenticationOptions = new AuthenticationOptions
                    {
                        IdentityProviders = ConfigureAdditionalIdentityProviders,
                    }
                };

                core.UseIdentityServer(options);
            });
        }

        public static void ConfigureAdditionalIdentityProviders(IAppBuilder app, string signInAsType)
        {
            var google = new GoogleOAuth2AuthenticationOptions
            {
                AuthenticationType = "Google",
                SignInAsAuthenticationType = signInAsType,
                ClientId = "767400843187-8boio83mb57ruogr9af9ut09fkg56b27.apps.googleusercontent.com",
                ClientSecret = "5fWcBT0udKY7_b6E3gEiJlze"
            };
            app.UseGoogleAuthentication(google);

            var fb = new FacebookAuthenticationOptions
            {
                AuthenticationType = "Facebook",
                SignInAsAuthenticationType = signInAsType,
                AppId = "676607329068058",
                AppSecret = "9d6ab75f921942e61fb43a9b1fc25c63"
            };
            app.UseFacebookAuthentication(fb);

            var twitter = new TwitterAuthenticationOptions
            {
                AuthenticationType = "Twitter",
                SignInAsAuthenticationType = signInAsType,
                ConsumerKey = "N8r8w7PIepwtZZwtH066kMlmq",
                ConsumerSecret = "df15L2x6kNI50E4PYcHS0ImBQlcGIt6huET8gQN41VFpUCwNjM"
            };
            app.UseTwitterAuthentication(twitter);
        }
    }
}