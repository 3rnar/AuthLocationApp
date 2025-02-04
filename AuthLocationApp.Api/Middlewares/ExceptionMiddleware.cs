using AuthLocationApp.Domain.Exceptions;
using Serilog;
using System.Net;
using System.Text.Json;

namespace AuthLocationApp.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (FluentValidation.ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (DomainException ex)
            {
                await HandleDomainExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleValidationExceptionAsync(HttpContext context, FluentValidation.ValidationException exception)
        {
            Log.Warning("Validation failed: {Errors}", string.Join("; ", exception.Errors.Select(e => e.ErrorMessage)));

            var response = new
            {
                message = "Validation failed",
                errors = exception.Errors.Select(e => e.ErrorMessage),
                statusCode = (int)HttpStatusCode.BadRequest // 400
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static async Task HandleDomainExceptionAsync(HttpContext context, DomainException exception)
        {
            Log.Warning("Business logic error: {Message}", exception.Message);

            var response = new
            {
                message = "Business rule violation",
                error = exception.Message,
                statusCode = (int)HttpStatusCode.UnprocessableEntity // 422
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Log.Error(exception, "An unhandled exception occurred");

            var response = new
            {
                message = "An unexpected error occurred.",
                error = exception.Message,
                statusCode = (int)HttpStatusCode.InternalServerError // 500
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.statusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
