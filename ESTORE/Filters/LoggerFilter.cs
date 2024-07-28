using Microsoft.AspNetCore.Mvc.Filters;

namespace ESTORE.Filters
{
    public class LoggerFilter : IActionFilter
    {
        private readonly ILogger<LoggerFilter> _logger;   
        public LoggerFilter(ILogger<LoggerFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Action Executed");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Action Executing");
        }
    }
}
