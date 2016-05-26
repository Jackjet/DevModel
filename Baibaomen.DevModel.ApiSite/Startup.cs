using Owin;
using System;

namespace Baibaomen.DevModel.ApiSite
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            WebApiConfig.Configure(app);
        }
    }
}