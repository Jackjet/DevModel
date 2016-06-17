using Autofac;

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
        }
    }
}
