using System.ComponentModel.DataAnnotations;
using CQRS_University_System.Application.Commons.Exceptions;

namespace CQRS_University_System.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            catch (FluentValidation.ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation failed. Path: {Path}", context.Request.Path);

                var errorMessages = ex.Errors.Select(e => e.ErrorMessage);

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { errors = errorMessages });
            }

            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Bad request: invalid argument. Path: {Path}", context.Request.Path);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Resource not found. Path: {Path}", context.Request.Path);
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogError(ex, "Request was canceled by client or server. Path: {Path}", context.Request.Path);
                context.Response.StatusCode = StatusCodes.Status499ClientClosedRequest;
                await context.Response.WriteAsJsonAsync(new { error = "Request was canceled." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred. Path: {Path}", context.Request.Path);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(ex);
            }
        }
    }
}
