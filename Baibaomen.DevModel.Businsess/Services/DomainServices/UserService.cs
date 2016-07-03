using Baibaomen.DevModel.Businsess.DataServices;
using Baibaomen.DevModel.Businsess.Entities;
using Baibaomen.DevModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Baibaomen.DevModel.Businsess.DomainServices
{

    /// <summary>
    /// 
    /// </summary>
    public static class UserServiceExtention
    {

        /// <summary>
        /// Get current operator based on ClaimsPrincipal
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public static User GetOperator(this ApiController me, UserService userDataService)
        {
            var user = me.User as ClaimsPrincipal;
            if (user == null)
            {
                return null;
            }

            var key = user.GetClaimIdentity();

            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            return userDataService.GetUserByClaimIdentityAsync(key).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserService
    {
        UserDataService _userDataService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDataService"></param>
        public UserService(UserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claimIdentity"></param>
        /// <returns></returns>
        public async Task<User> GetUserByClaimIdentityAsync(string claimIdentity)
        {
            return await _userDataService.GetUserByClaimIdentityAsync(claimIdentity);
        }
    }
}
