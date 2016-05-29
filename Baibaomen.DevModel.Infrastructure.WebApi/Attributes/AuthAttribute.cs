//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Http;

//namespace Baibaomen.DevModel.Infrastructure.WebApi.Attributes
//{
//    public class AuthAttribute : AuthorizeAttribute
//    {
//        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
//        {
//            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
//            {
//                // 403 we know who you are, but you haven't been granted access
//                filterContext.Result = new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
//            }

//            else
//            {
//                // 401 who are you? go login and then try again
//                filterContext.Result = new HttpUnauthorizedResult();
//            }
//        }
//    }
//}
