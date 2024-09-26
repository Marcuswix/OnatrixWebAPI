using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnatrixWebAPI.MiddleWares
{
    public class ApiKeyFilter : IActionFilter
    {
        private readonly IConfiguration _configuration;

        public ApiKeyFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var apiKey = _configuration["Values:ApiKey"];

            if (apiKey != null)
            {
                if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var extractedApiKey))
                {
                    if (apiKey == extractedApiKey)
                    {
                        return;
                    }
                    context.Result = new UnauthorizedResult();
                    return;
                }
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
