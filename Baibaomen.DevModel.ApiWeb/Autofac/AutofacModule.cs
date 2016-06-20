using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;

namespace Baibaomen.DevModel.ApiWeb
{
    /// <summary>
    /// 
    /// </summary>
    public class AutofacModule: Autofac.Module
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }
    }
}