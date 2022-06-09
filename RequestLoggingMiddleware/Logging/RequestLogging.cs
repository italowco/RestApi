using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace RequestLoggingMiddleware.Logging
{
    public class RequestLogging
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLogging(RequestDelegate next, ILoggerFactory logger)
        {
            _next = next;
            _logger = logger.CreateLogger<RequestLogging>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                DateTime dateTime = DateTime.Now;

                _logger.LogInformation(
                    "[{dateTime}] Request {method} {url} => {statusCode}",
                    dateTime,
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode
                    );

                //_logger.LogError("Stay out of my territory");

            }
        }
    }
}