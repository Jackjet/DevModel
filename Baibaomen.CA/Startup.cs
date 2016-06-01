using IdentityManager.Configuration;
using IdentityServer3.Core.Configuration;
using Owin;
using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace Baibaomen.CA
{
    public class Startup
    {
        static string conn = ConfigurationManager.AppSettings["DBConnection"];

        public void Configuration(IAppBuilder app)
        {
            var options = new IdentityServerOptions
            {
                SiteName = "IdentityServer",
                SigningCertificate = LoadCertificate(),
                //Factory = new IdentityServerServiceFactory()
                //            .UseInMemoryClients(ClientService.Get())
                //            .UseInMemoryScopes(ScopeService.Get())
                //            .UseInMemoryUsers(UserService.Get()),
                Factory = new IdentityServerServiceFactory().Configure(conn),
                RequireSsl = true
            };
            
            app.Map("/admin", adminApp => {
                adminApp.UseIdentityManager(new IdentityManagerOptions()
                {
                    Factory = new IdentityManagerServiceFactory().Configure(conn)
                });
            });

            app.UseIdentityServer(options);
        }

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\App_Data\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}