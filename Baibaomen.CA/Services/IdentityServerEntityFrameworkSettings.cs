using IdentityServer3.AspNetIdentity;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Baibaomen.CA
{

    public class Context : IdentityDbContext<IdentityUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public Context(string connectionString) : base(connectionString) { }
    }

    public class UserStore : UserStore<IdentityUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public UserStore(Context context) : base(context) { }
    }

    public class RoleStore : RoleStore<IdentityRole>
    {
        public RoleStore(Context context) : base(context) { }
    }


    public class UserManager : UserManager<IdentityUser, string>
    {
        public UserManager(UserStore userStore)
            : base(userStore)
        {
        }
    }

    public class RoleManager : RoleManager<IdentityRole>
    {
        public RoleManager(RoleStore roleStore) : base(roleStore) { }
    }

    public class IdentityUserService : AspNetIdentityUserService<IdentityUser, string>
    {
        public IdentityUserService(UserManager userManager) : base(userManager)
        {
        }
    }

    public static class IdentityServerServiceFactoryExtensions
    {
        public static IdentityServerServiceFactory Configure(this IdentityServerServiceFactory factory, string connectionString)
        {

            var serviceOptions = new EntityFrameworkServiceOptions { ConnectionString = connectionString };
            factory.RegisterOperationalServices(serviceOptions);
            factory.RegisterConfigurationServices(serviceOptions);

            factory.Register(new Registration<Context>(resolver => new Context(connectionString)));
            factory.Register(new Registration<UserStore>());
            factory.Register(new Registration<UserManager>());
            factory.UserService = new Registration<IUserService, IdentityUserService>();

            return factory;

        }
    }
}