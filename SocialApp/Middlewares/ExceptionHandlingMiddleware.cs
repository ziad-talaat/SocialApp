using FluentValidation;
using Microsoft.AspNetCore.Mvc;
namespace SocialApp.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public  async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
              await  _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went wrong: {Message}", ex.Message);

              

                 var validationException=  ex as ValidationException;
                    
                if(validationException != null)
                {
                    var validationProblemDetails = new ValidationProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Validation Failed",
                        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section~6.6.1",
                        Detail = $"Some validation Properties have failed",
                        Errors = validationException.Errors
                        .GroupBy(x => x.PropertyName)
                        .ToDictionary(g => g.Key.Split('.')[1],g=>g.Select(x=>x.ErrorMessage).ToArray())
                         
                    };
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await httpContext.Response.WriteAsJsonAsync(validationProblemDetails);
                    return;
                }


                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section~6.6.1",
                    Detail = $"Something went wrong: {ex.Message}"
                };
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
