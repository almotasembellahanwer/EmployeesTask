using System.Net;

namespace EmployeeTask.Core.Exceptions;

public class AppException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    public AppException(string message,HttpStatusCode statusCode = HttpStatusCode.InternalServerError,Exception? innerException = null)
        : base(message,innerException)
    {
        StatusCode = statusCode;
    }

}
