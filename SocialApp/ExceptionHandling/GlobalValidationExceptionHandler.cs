using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SocialApp.ExceptionHandling
{
    public sealed  class GlobalValidationExceptionHandler(ILogger<GlobalValidationExceptionHandler>_logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if(exception is not  ValidationException validationException)
            {
                return false;
            }
             
            _logger.LogError(exception, "Validation Failed");

                var validationProblemDetails = new ValidationProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation Failed",
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section~6.6.1",
                    Detail = $"Some validation Properties have failed",
                    Errors = validationException.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(g => g.Key.Split('.').Last(), g => g.Select(x => x.ErrorMessage).ToArray())

                };
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(validationProblemDetails, cancellationToken: cancellationToken);
            return true;
        }
    }
}
