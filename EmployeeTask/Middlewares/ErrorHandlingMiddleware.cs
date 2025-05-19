using EmployeeTask.Core.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeTask.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                if(ex.InnerException is not null)
                {
                    _logger.LogError("{exceptionType} : {exceptionMessage}", ex.InnerException.GetType().Name, ex.InnerException.Message);

                }
                else
                {
                    _logger.LogError("{exceptionType} : {exceptionMessage}", ex.GetType().Name, ex.Message);

                }
                await HandleExceptionAsync(ex,httpContext);

            }

        }

        private async Task HandleExceptionAsync(Exception ex,HttpContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = ex.Message;
            var details = ex.InnerException?.Message;
            switch (ex)
            {
                case NotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case BadRequestException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case AppException:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    statusCode = HttpStatusCode.InternalServerError;
                    break;

            }

            var errorResponse = new
            {
                Type = ex.GetType().Name,
                StatusCode = statusCode,
                Message = message,
                Details = details
            };
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
