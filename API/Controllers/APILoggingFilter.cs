using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace API.Controllers
{
    public class APILoggingFilter : IActionFilter
    {

        private readonly ILogger<APILoggingFilter> _logger;
        private Stopwatch _stopwatch;
        public APILoggingFilter(ILogger<APILoggingFilter> logger) 
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
           _logger.LogInformation($"\n      API Action {context.ActionDescriptor.DisplayName} Starting...");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();
            _logger.LogInformation($"API Action {context.ActionDescriptor.DisplayName} finished in {_stopwatch.ElapsedMilliseconds} ms \n" );
        }
    }
}
