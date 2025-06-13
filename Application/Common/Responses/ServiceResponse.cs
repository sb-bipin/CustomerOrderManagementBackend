using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Responses
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();
        public int StatusCode { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string ExceptionDetails { get; set; }

        private ServiceResponse() { }

        public static ServiceResponse<T> SuccessResponse(T data, string message = "", int statusCode = (int)HttpStatusCode.OK)
        {
            return new ServiceResponse<T>
            {
                Data = data,
                Success = true,
                Message = message,
                StatusCode = statusCode
            };
        }

        public static ServiceResponse<T> FailureResponse(string error, string message = "", int statusCode = (int)HttpStatusCode.BadRequest)
        {
            return new ServiceResponse<T>
            {
                Success = false,
                Errors = new List<string> { error },
                Message = message,
                StatusCode = statusCode
            };
        }

        public static ServiceResponse<T> FailureResponse(List<string> errors, string message = "", int statusCode = (int)HttpStatusCode.BadRequest)
        {
            return new ServiceResponse<T>
            {
                Success = false,
                Errors = errors,
                Message = message,
                StatusCode = statusCode
            };
        }

        public static ServiceResponse<T> NotFoundResponse(string message = "Not Found")
        {
            return new ServiceResponse<T>
            {
                Success = false,
                Message = message,
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }

        public static ServiceResponse<T> ExceptionResponse(Exception ex, string message = "An unexpected error occurred")
        {
            return new ServiceResponse<T>
            {
                Success = false,
                Message = message,
                ExceptionDetails = ex.Message,
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
