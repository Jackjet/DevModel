using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Baibaomen.FileService.Services;

namespace Biabaomen.FileService.Controllers
{
    /// <summary>
    /// File controller.
    /// </summary>
    [RoutePrefix("api/files")]
    public class FileController : ApiController
    {
        private readonly Baibaomen.FileService.Services.FileService _fileService = new Baibaomen.FileService.Services.FileService();
        private readonly FilePathService _filePathService = new FilePathService();

        ///// <summary>
        ///// Initializes a new instance of the <see cref="FileController" /> class.
        ///// </summary>
        ///// <param name="fileService">The file service.</param>
        ///// <param name="filePathService">The file path service.</param>
        //public FileController(Baibaomen.FileService.Services.FileService fileService, FilePathService filePathService)
        //{
        //    _fileService = fileService;
        //    _filePathService = filePathService;
        //}

        /// <summary>
        /// Gets the private file.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <response code="200">The audience got.</response>
        /// <response code="401">Unauthorized.</response>
        [Authorize]
        [Route("private/{id}",Name="GetPrivateFile")]
        public async Task<HttpResponseMessage> GetPrivate(string id)
        {
            var userId = User.Identity.Name;
            var bucket = _filePathService.PrivateBucket;
            var key = _filePathService.GetPrivateKey(id, userId);
            return await _fileService.Get(bucket, key);
        }

        /// <summary>
        /// Gets the protected file.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <response code="200">The audience got.</response>
        /// <response code="401">Unauthorized.</response>
        [Authorize]
        [Route("protected/{id}", Name = "GetProtectedFile")]
        public async Task<HttpResponseMessage> GetProtected(string id)
        {
            var bucket = _filePathService.ProtectedBucket;
            var key = _filePathService.GetProtectedKey(id);
            return await _fileService.Get(bucket, key);
        }

        /// <summary>
        /// Posts the private file.
        /// </summary>
        /// <response code="201">The file created.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="415">Unsupported media type.</response>
        [Authorize]
        [Route("private")]
        public async Task<IHttpActionResult> PostPrivate()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return StatusCode(HttpStatusCode.UnsupportedMediaType);
            }
            var id = Guid.NewGuid().ToString("N");
            var bucket = _filePathService.PrivateBucket;
            var key = _filePathService.GetPrivateKey(id,User.Identity.Name);
            await _fileService.Add(Request,bucket, key);

            var requestScheme = Request.RequestUri.Scheme;
            var requestAuthority = Request.RequestUri.Authority;
            var fileUrl = requestScheme + "://" + requestAuthority + Url.Route("GetPrivateFile", new { id });
            return Created(fileUrl, fileUrl);
        }

        /// <summary>
        /// Posts the protected file.
        /// </summary>
        /// <response code="201">The file created.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="415">Unsupported media type.</response>
        [Authorize]
        [Route("protected")]
        public async Task<IHttpActionResult> PostProtected()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return StatusCode(HttpStatusCode.UnsupportedMediaType);
            }
            var id = Guid.NewGuid().ToString("N");
            var bucket = _filePathService.ProtectedBucket;
            var key = _filePathService.GetProtectedKey(id);
            await _fileService.Add(Request,bucket, key);
            var requestScheme = Request.RequestUri.Scheme;
            var requestAuthority = Request.RequestUri.Authority;
            var fileUrl = requestScheme + "://"+ requestAuthority + Url.Route("GetProtectedFile", new { id });
            return Created(fileUrl, fileUrl);
        }

        /// <summary>
        /// Posts the public file.
        /// </summary>
        /// <response code="201">The file created.</response>
        /// <response code="415">Unsupported media type.</response>
        [Authorize]
        [Route("public")]
        public async Task<IHttpActionResult> PostPublic()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return StatusCode(HttpStatusCode.UnsupportedMediaType);
            }
            var id = Guid.NewGuid().ToString("N");
            var bucket = _filePathService.PublicBucket;
            var key = _filePathService.GetPublicKey(id);
            await _fileService.Add(Request,bucket, key);
            var fileUrl = _filePathService.GetPublicFileUrl(id);
            return Created(fileUrl, fileUrl);
        }

        /// <summary>
        /// Deletes the protected.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <response code="204">The file deleted.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">The file not found.</response>
        [Authorize]
        [Route("protected/{id}")]
        public async Task DeleteProtected(string id)
        {
            var key = _filePathService.GetProtectedKey(id);
            var bucket = _filePathService.ProtectedBucket;
            await _fileService.Delete(bucket, key);
        }

        /// <summary>
        /// Deletes the public.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <response code="204">The file deleted.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">The file not found.</response>
        [Authorize]
        [Route("public/{id}")]
        public async Task DeletePublic(string id)
        {
            var key = _filePathService.GetPublicKey(id);
            var bucket = _filePathService.PublicBucket;
            await _fileService.Delete(bucket, key);
        }
    }
}
