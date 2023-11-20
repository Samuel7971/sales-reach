using System.Net;

namespace SalesReachAPI.Interface.Controllers.Shared
{
    /// <summary>
    /// Custom result 
    /// </summary>
    public class CustomResult
    {
        public HttpStatusCode StatusCode { get; private set; }
        public bool Success { get; set; }
        public object Data { get; set; }
        public IEnumerable<string> Errors { get; set; }

        /// <summary>
        /// Contrutor
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="success"></param>
        public CustomResult(HttpStatusCode statusCode, bool success)
        {
            StatusCode = statusCode;
            Success = success;
        }

        public CustomResult(HttpStatusCode statusCode, bool success, object data) : this(statusCode, success)
            => Data = data;

        public CustomResult(HttpStatusCode statusCode, bool success, IEnumerable<string> errors) : this(statusCode, success)
            => Errors = errors;

        public CustomResult(HttpStatusCode statusCode, bool success, object data, IEnumerable<string> errors) : this(statusCode, success, data) =>
            Errors = errors;
    }
}
