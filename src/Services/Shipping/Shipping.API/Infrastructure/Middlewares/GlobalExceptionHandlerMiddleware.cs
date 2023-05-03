using Microsoft.Extensions.Logging;
using Shipping.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace Shipping.API.Infrastructure.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        public GlobalExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(ex.HResult),
                    ex,
                    ex.Message);

                var response = context.Response;
                response.ContentType = "application/json";
                var errorResponse = new ErrorResponse
                {
                    Errors = new string[] { "An error occur.Try it again." }
                };

                switch (ex)
                {
                    case ShippingDomainException e:
                        errorResponse.Errors = new string[] { e.Message.ToString() };
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        if (_env.IsDevelopment())
                        {
                            errorResponse.DeveloperMessage = ex.Message;
                        }
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(errorResponse);
                await response.WriteAsync(result);
            }
        }
        private class ErrorResponse
        {
            public string[] Errors { get; set; } = Array.Empty<string>();
            public string? DeveloperMessage { get; set; }
        }
    }
}
