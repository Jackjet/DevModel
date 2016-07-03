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
        /// Gets the user id from claim 'sub'. Returns null if no sub claim found.
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public static string GetClaimIdentity(this ClaimsPrincipal me) {
            var sub = me.FindFirst("sub");
            return sub == null ? null : sub.Value;
        }
    }
}
