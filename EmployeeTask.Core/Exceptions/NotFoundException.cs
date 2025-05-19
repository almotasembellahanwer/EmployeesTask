using System.Net;

namespace EmployeeTask.Core.Exceptions;
public class NotFoundException : AppException
{
    public NotFoundException(string message, HttpStatusCode statusCode = HttpStatusCode.NotFound) : base(message)
    {
        
    }
}
