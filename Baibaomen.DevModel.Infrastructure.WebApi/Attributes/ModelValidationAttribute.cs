using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Baibaomen.DevModel.Infrastructure
{
    /// <summary>
    /// Validate the model. Return badrequest and the modelstate if invalid.
    /// </summary>
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }

    public static class ModelValidationAttributeExtention
    {
        /// <summary>
        /// Add model validation logic to all api controllers. Will return BadRequest and the modelstate to client.
        /// </summary>
        /// <param name="config"></param>
        public static void AddModelValidationFilter(this HttpConfiguration config)
        {
            config.Filters.Add(new ModelValidationAttribute());
        }
    }
}
