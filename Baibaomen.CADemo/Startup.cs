using IdentityServer3.Core.Configuration;
using Owin;
using System;
using System.Configuration;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using System.Web.Http.Routing;

public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        ConfigureWebApi(app);
    }
    
    static void ConfigureWebApi(IAppBuilder app)
    {
        HttpConfiguration config = new HttpConfiguration();

        // Web API routes
        config.MapHttpAttributeRoutes();

        // Json formatter
        config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

        app.UseWebApi(config);
    }

    static void ConfigureIdentityServer(IAppBuilder app) {
        app.Map("/identity", id => {
            id.UseIdentityServer(new IdentityServerOptions
            {
                SiteName = "Demo Identity Server",
                SigningCertificate = LoadCertificate()

                // Factory =  TODO
            });

        });
    }

    static X509Certificate2 LoadCertificate()
    {

        //Test certificate sourced from https://github.com/IdentityServer/IdentityServer3.Samples/tree/master/source/Certificates
        return new X509Certificate2(
            string.Format(@"{0}\bin\{1}", AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["signing-certificate.name"]),
            (string)ConfigurationManager.AppSettings["signing-certificate.password"]);
    }
}