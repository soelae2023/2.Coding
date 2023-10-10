using System.Net;
using System.Text.Json;
using SimpleApISystem.Exceptions;

namespace SimpleApISystem.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (PayloadValidationException validationException)
            {
                _logger.LogError(validationException, "Payload validation error.");
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, validationException.Message);
            }
            catch (DatabaseOperationException dbException)
            {
                _logger.LogError(dbException, "Database operation error.");
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, dbException.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "An unexpected error occurred.");
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new { error = message });
            return context.Response.WriteAsync(result);
        }
    }

}
