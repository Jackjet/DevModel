using IdentityServer3.Core.Configuration;
using Owin;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Baibaomen.CA
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var options = new IdentityServerOptions
            {
                SiteName = "IdentityServer",
                SigningCertificate = LoadCertificate(),
                Factory = new IdentityServerServiceFactory()
                            .UseInMemoryClients(ClientService.Get())
                            .UseInMemoryScopes(ScopeService.Get())
                            .UseInMemoryUsers(UserService.Get()),
                RequireSsl = true
            };

            //app.Map("/identity", identity =>
            //{
            //    identity.UseIdentityServer(options);
            //});

            app.UseIdentityServer(options);
        }

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\App_Data\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}