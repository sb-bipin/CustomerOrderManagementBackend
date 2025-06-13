using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Application.Common.Responses
{
    public class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception is ValidationException validationException)
            {
                context.Response.StatusCode = (int)422;

                var response = new
                {
                    Success = false,
                    Message = "Validation failed",
                    Errors = validationException.Errors.Select(e => e.ErrorMessage).ToList()
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                return;
            }

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var genericResponse = new
            {
                Success = false,
                Message = "An unexpected error occurred.",
                Error = exception.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(genericResponse));
        }
    }
}
