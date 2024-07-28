namespace ESTORE.Middlewares
{
    public class LogginMiddleware
    {
        private readonly ILogger<LogginMiddleware> _logger;
        private readonly RequestDelegate _next;
        public LogginMiddleware(RequestDelegate requestDelegate, ILogger<LogginMiddleware> logger)
        {
             _logger = logger;
            _next = requestDelegate; 
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"Incoming Request: {context.Request.Method} {context.Request.Path}");
            await _next(context);
            _logger.LogInformation($"Outgoing Response: {context.Response.StatusCode}");

        }
    }
}
