using System.Configuration;

namespace Baibaomen.FileService.Services
{
    /// <summary>
    /// File path service.
    /// </summary>
    public class FilePathService
    {
        private readonly string _awsRegion = ConfigurationManager.AppSettings["AWSRegion"];
        private readonly string _thePrivateFilePath = ConfigurationManager.AppSettings["file:PrivatePath"];
        private readonly string _theProtectedFilePath = ConfigurationManager.AppSettings["file:ProtectedPath"];
        private readonly string _thePublicFilePath = ConfigurationManager.AppSettings["file:PublicPath"];

        /// <summary>
        /// Initializes a new instance of the <see cref="FilePathService"/> class.
        /// </summary>
        public FilePathService()
        {
            PrivateBucket = GetBucket(_thePrivateFilePath);
            ProtectedBucket = GetBucket(_theProtectedFilePath);
            PublicBucket = GetBucket(_thePublicFilePath);
        }

        /// <summary>
        /// Gets or sets the private bucket.
        /// </summary>
        public string PrivateBucket { get; set; }

        /// <summary>
        /// Gets or sets the protected bucket.
        /// </summary>
        public string ProtectedBucket { get; set; }

        /// <summary>
        /// Gets or sets the public bucket.
        /// </summary>
        public string PublicBucket { get; set; }

        /// <summary>
        /// Gets the private key.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public string GetPrivateKey(string id, string userId)
        {
            return GetFolder(_thePrivateFilePath) + "/" + userId + "/" + id;
        }

        /// <summary>
        /// Gets the protected key.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public string GetProtectedKey(string id)
        {
            return GetFolder(_theProtectedFilePath) + "/" + id;
        }

        /// <summary>
        /// Gets the public key.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public string GetPublicKey(string id)
        {
            return GetFolder(_thePublicFilePath) + "/" + id;
        }

        /// <summary>
        /// Gets the public file URL.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="isSsl">if set to <c>true</c> [is SSL].</param>
        public string GetPublicFileUrl(string id, bool isSsl = false)
        {
            var region = "-" + _awsRegion;
            if (_awsRegion == "us-east-1")
            {
                region = string.Empty;
            }
            return (isSsl ? "https" : "http") + "://s3" + region + ".amazonaws.com/" + _thePublicFilePath + "/" + id;
        }

        private string GetBucket(string filePath)
        {
            var length = filePath.IndexOf('/');
            return filePath.Substring(0, length);
        }

        private string GetFolder(string filePath)
        {
            var start = filePath.IndexOf('/') + 1;
            return filePath.Substring(start);
        }
    }
}