using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Baibaomen.DevModel
{
    /// <summary>
    /// Swagger OAuth2 Security Requirment
    /// </summary>
    internal class OAuth2OperationFilter : IOperationFilter
    {
        /// <summary>
        /// Apply OAuth2
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiDescription"></param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            // Correspond each "Authorize" role to an oauth2 scope
            var scopes = apiDescription.ActionDescriptor.GetFilterPipeline()
                .Select(filterInfo => filterInfo.Instance)
                .OfType<AuthorizeAttribute>()
                .SelectMany(attr => attr.Roles.Split(','))
                .Distinct();

            if (scopes.Any())
            {
                if (operation.security == null)
                    operation.security = new List<IDictionary<string, IEnumerable<string>>>();

                var oAuthRequirements = new Dictionary<string, IEnumerable<string>>
                {
                    { "oauth2", scopes }
                };

                operation.security.Add(oAuthRequirements);
            }
        }
    }
}