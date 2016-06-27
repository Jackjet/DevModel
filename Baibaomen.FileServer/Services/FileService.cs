using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Amazon.S3;
using Amazon.S3.Model;

namespace Baibaomen.FileService.Services
{
    /// <summary>
    /// File service.
    /// </summary>
    public class FileService
    {
        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="bucket">The bucket.</param>
        /// <param name="key">The key.</param>
        public async Task<HttpResponseMessage> Get(string bucket, string key)
        {
            using (var amazonS3Client = new AmazonS3Client())
            {
                var getObjectRequest = new GetObjectRequest
                {
                    BucketName = bucket,
                    Key = key
                };
                var objectResponse = await amazonS3Client.GetObjectAsync(getObjectRequest);
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                foreach (var name in objectResponse.Metadata.Keys)
                {
                    response.Headers.TryAddWithoutValidation(name, objectResponse.Metadata[name]);
                }
                response.Content = new StreamContent(objectResponse.ResponseStream);
                if (!string.IsNullOrEmpty(objectResponse.Headers.ContentType))
                {
                    response.Content.Headers.Add("Content-Type", objectResponse.Headers.ContentType);
                    response.Content.Headers.Add("Content-Disposition", 
                        $"attachment;filename=\"{HttpUtility.UrlDecode(objectResponse.Metadata["x-amz-meta-filename"])}\"");
                }
                return response;
            }
        }

        /// <summary>
        /// Adds the file.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="bucket">The bucket.</param>
        /// <param name="key">The key.</param>
        public async Task Add(HttpRequestMessage request,string bucket, string key)
        {
            var memoryStreamProvider = await request.Content.ReadAsMultipartAsync();
            var content = memoryStreamProvider.Contents.First(c => !string.IsNullOrEmpty(c.Headers.ContentDisposition.FileName));
            var fileName = content.Headers.ContentDisposition.FileName.Trim('\"');
            var contentType = MimeMapping.GetMimeMapping(fileName);
            var inputStream = await content.ReadAsStreamAsync();
            var putObjectRequest = new PutObjectRequest
            {
                BucketName = bucket,
                Key = key,
                InputStream = inputStream,
                ContentType = contentType,
                Headers =
                {
                    ContentDisposition =
                    $"attachment; filename =\"{HttpUtility.UrlEncode(fileName, Encoding.UTF8)}\""
                }
            };
            putObjectRequest.Metadata.Add("FileName", HttpUtility.UrlEncode(fileName, Encoding.UTF8));
            putObjectRequest.Metadata.Add("UploadTime", DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
            using (var amazonS3Client = new AmazonS3Client())
            {
                await amazonS3Client.PutObjectAsync(putObjectRequest);
            }
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="bucket">The bucket.</param>
        /// <param name="key">The key.</param>
        public async Task Delete(string bucket, string key)
        {
            DeleteObjectRequest deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = bucket,
                Key = key
            };
            using (var amazonS3Client = new AmazonS3Client())
            {
                await amazonS3Client.DeleteObjectAsync(deleteObjectRequest);
            }
        }
    }
}