using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Baibaomen.DevModel.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public static class ClaimsExtentionForApiController
    {
        /// <summary>
        /// return me.RequestContext.Principal as ClaimsPrincipal
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public static ClaimsPrincipal GetClaimsPrincipal(this ApiController me) {
            return me.RequestContext.Principal as ClaimsPrincipal;
        }

        /// <summary>
        /// returns null if no user principal found.
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public static string GetUserId(this ClaimsPrincipal me) {
        }
    }
}
