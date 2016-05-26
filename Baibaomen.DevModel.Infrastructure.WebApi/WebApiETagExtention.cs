using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Baibaomen.DevModel.Infrastructure
{
    public static class WebApiETagExtention
    {
        /// <summary>
        /// Add Etag support to dynamic output.
        /// </summary>
        /// <param name="me"></param>
        /// <param name="prefixes">Specify the local path prefixes.</param>
        /// <param name="prefixesHandlingMode">Sperify how the requests matched the prefixes are to be handled.</param>
        public static void EnableETag(this HttpConfiguration me, List<string> prefixes, ExecutionMode prefixesHandlingMode) {
            me.MessageHandlers.Add(new ETagHandler(prefixesHandlingMode, prefixes));
        }
    }

    /// <summary>
    /// Specify how the prefixes are handled in ETagHandler.
    /// </summary>
    public enum ExecutionMode
    {
        Include,
        Exclude
    }

    /// <summary>
    /// Add ETag support to dynamic output.
    /// </summary>
    class ETagHandler : DelegatingHandler
    {

        private List<string> prefixes;
        ExecutionMode mode;

        public ETagHandler(ExecutionMode mode, List<string> prefixes) {
            this.mode = mode;
            this.prefixes = prefixes;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            if (request.Method == HttpMethod.Get)
            {
                if (response.Content == null)
                {
                    return response;
                }

                var localPath = request.RequestUri.LocalPath;
                if (mode == ExecutionMode.Include)
                {
                    var matchFound = false;
                    foreach (var path in prefixes)
                    {
                        if (localPath.StartsWith(path))
                        {
                            matchFound = true;
                            break;
                        }
                    }

                    if (!matchFound)
                    {
                        return response;
                    }
                }
                else
                {
                    foreach (var path in prefixes)
                    {
                        if (localPath.StartsWith(path)) {
                            return response;
                        }
                    }
                }

                var hash = MD5.Create().ComputeHash(await response.Content.ReadAsStreamAsync());
                var etag = $"\"{Convert.ToBase64String(hash)}\"";
                response.Headers.ETag = new EntityTagHeaderValue(etag);
                var matchETag = request.Headers.IfNoneMatch.FirstOrDefault();
                if (matchETag != null && matchETag.Tag == etag)
                {
                    response = request.CreateResponse(HttpStatusCode.NotModified);
                }
            }

            return response;
        }
    }
}
