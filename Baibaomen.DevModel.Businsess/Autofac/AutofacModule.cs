using Autofac;
using Baibaomen.DevModel.Businsess.ComponentServices;
using Baibaomen.DevModel.Businsess.DBContexts;

namespace Baibaomen.DevModel.Businsess
{
    public class AutofacModule: Autofac.Module
    {
        /// <summary>
        ///     Loads to the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlDbContext>().AsSelf().InstancePerLifetimeScope();
            builder.Register(c => new SnSService("some sns configuration", x => $"{x}@test.com", x => $"mobile:{x}")).SingleInstance();
        }
    }
}
