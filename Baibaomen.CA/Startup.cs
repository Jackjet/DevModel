using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services.InMemory;
using Owin;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using IdentityServer3.Core.Models;

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
                            .UseInMemoryScopes(ScopeService.Get().Concat(StandardScopes.All))
                            .UseInMemoryUsers(UserService.Get()),

                RequireSsl = true
            };

            app.UseIdentityServer(options);
        }

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\App_Data\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}