using IdentityManager;
using IdentityManager.AspNetIdentity;
using IdentityManager.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Baibaomen.CA
{
    public class IdentityManagerService<TUser, TUserKey, TRole, TRoleKey> : AspNetIdentityManagerService<TUser, TUserKey, TRole, TRoleKey> where TUser : class, IUser<TUserKey>, new()
        where TUserKey : IEquatable<TUserKey>
        where TRole : class, IRole<TRoleKey>, new()
        where TRoleKey : IEquatable<TRoleKey>
    {
        public IdentityManagerService(UserManager<TUser, TUserKey> userManager, RoleManager<TRole, TRoleKey> roleManager)
            : base(userManager, roleManager)
        {
        }
    }

    public static class IdentityManagerServiceExtensions
    {
        public static IdentityManagerServiceFactory Configure(this IdentityManagerServiceFactory factory, string connectionString)
        {
            //factory.Register(new Registration<Context>(resolver => new Context(connectionString)));
            //factory.Register(new Registration<UserStore>());
            //factory.Register(new Registration<RoleStore>());
            //factory.Register(new Registration<UserManager>());
            //factory.Register(new Registration<RoleManager>());

            //factory.IdentityManagerService = new Registration<IdentityManager.IIdentityManagerService, IdentityManagerService<IdentityUser,string,IdentityRole,string>>();

            //return factory;
            
            factory.Register(new Registration<IdentityDbContext>());
            factory.Register(new Registration<UserStore<IdentityUser>>());
            factory.Register(new Registration<RoleStore<IdentityRole>>());
            factory.Register(new Registration<UserManager<IdentityUser, string>>(x => new UserManager<IdentityUser>(x.Resolve<UserStore<IdentityUser>>())));
            factory.Register(new Registration<RoleManager<IdentityRole, string>>(x => new RoleManager<IdentityRole>(x.Resolve<RoleStore<IdentityRole>>())));

            factory.IdentityManagerService = new Registration<IIdentityManagerService, AspNetIdentityManagerService<IdentityUser, string, IdentityRole, string>>();

            return factory;
        }
    }

}