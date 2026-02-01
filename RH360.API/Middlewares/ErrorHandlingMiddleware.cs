using FluentValidation;
using RH360.API.Exceptions;
using RH360.Domain.Extensions;
using System.Net;
using System.Text.Json;

namespace RH360.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            switch (ex)
            {
                case IdNotFoundException idNotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    var notFoundResponse = idNotFoundException.Message.ToError();
                    await context.Response.WriteAsync(JsonSerializer.Serialize(notFoundResponse));
                    break;
                case ValidationException validationException:
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

                    var response = new
                    {
                        message = "Validation failed",
                        errors = validationException.Errors
                            .GroupBy(e => e.PropertyName)
                            .ToDictionary(
                                g => g.Key,
                                g => g.Select(e => e.ErrorMessage).ToArray()
                            )
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    var generic = new
                    {
                        message = ex.Message,
                        stackTrace = ex.StackTrace
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(generic));
                    break;
            }
        }
    }
}
