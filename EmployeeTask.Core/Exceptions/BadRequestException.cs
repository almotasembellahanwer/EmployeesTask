using System.Net;

namespace EmployeeTask.Core.Exceptions;
public class BadRequestException : AppException
{
    public BadRequestException(string message,HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message)
    {
        
    }
}
