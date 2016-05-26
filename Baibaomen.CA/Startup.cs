using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services.InMemory;
using Owin;
using System;
using System.Collections.Generic;

namespace Baibaomen.CA
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var options = new IdentityServerOptions
            {
                Factory = new IdentityServerServiceFactory()
                            .UseInMemoryClients(ClientsService.Get())
                            .UseInMemoryScopes(ScopesService.Get())
                            .UseInMemoryUsers(new List<InMemoryUser>()),

                RequireSsl = false
            };

            app.UseIdentityServer(options);
        }
    }
}