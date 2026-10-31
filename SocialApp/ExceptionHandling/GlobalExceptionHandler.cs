using System;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
namespace SocialApp.ExceptionHandling
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler>_logger, IProblemDetailsService _problemDetailsService) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is  ValidationException )
                return false;

                _logger.LogError(exception, "Something Went wrong: {Message}", exception.Message);

            return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                Exception = exception,
                HttpContext = httpContext,
                ProblemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section~6.6.1",
                    Detail = $"Something went wrong: {exception.Message}",
                }
            });
        }
    }
}
