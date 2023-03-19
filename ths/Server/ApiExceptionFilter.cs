using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using ths.Infrastructure.Exceptions;
using ths.Server.Models;

namespace ths.Server
{
    public class ApiExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> _logger;
        private readonly RequestDelegate _next;
        private readonly IDictionary<Type, Action<HttpContext, Exception>> _exceptionHandlers;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
            _exceptionHandlers = new Dictionary<Type, Action<HttpContext, Exception>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(ApiException), HandleApiException },
            };
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                HandleException(context, ex);
            }
        }

        private void HandleException(HttpContext context, Exception exception)
        {
            var type = exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context, exception);
                return;
            }
        }


        protected virtual void HandleValidationException(HttpContext context, Exception exception)
        {
            var validationException = exception as ValidationException;

            _logger.LogInformation(validationException, "Validation Exception raised.");
            var details = new ValidationProblemDetails(validationException?.Errors)
            {
                Status = StatusCodes.Status400BadRequest,
                Type = nameof(ValidationException),
                Detail = "https://tools.ietf.org/html/rfc7231#section-6.5.4"

            };

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.WriteAsJsonAsync(details).Wait();
        }

        protected virtual void HandleApiException(HttpContext context, Exception exception)
        {
            var apiException = exception as ApiException;

            var details = new ApiProblemDetails(apiException?.Error.Message)
            {
                Status = StatusCodes.Status400BadRequest,
                Type = nameof(ApiException),
                Detail = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
            };

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(details)));
        }

    }
}
