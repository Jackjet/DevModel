using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Baibaomen.DevModel.Infrastructure
{
    public class SimpleConflictResult : IHttpActionResult
    {
        public SimpleConflictResult(HttpRequestMessage request, ErrorMessage errorMessage)
        {
            Request = request;
            ErrorMessage = errorMessage;
        }
        
        public HttpRequestMessage Request { get; }

        public ErrorMessage ErrorMessage { get; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = Request.CreateResponse(HttpStatusCode.Conflict, ErrorMessage);
            return Task.FromResult(response);
        }
    }
}
