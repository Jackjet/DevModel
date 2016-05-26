namespace Baibaomen.DevModel.Infrastructure
{
    /// <summary>
    /// Error message to return to client.
    /// </summary>
    public class ErrorMessage
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }
}