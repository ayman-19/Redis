using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Redis.CashService;
using System.Text;

namespace Redis.Helper
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly double _time;

        public CacheAttribute(double time)
        {
            _time = time;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var casheService = context.HttpContext.RequestServices.GetRequiredService<ICashService>();
            var key = GenerateCacheKey(context.HttpContext.Request);
            var result = await casheService.GetAsync(key);
            if (!string.IsNullOrEmpty(result))
            {
                context.Result = new ContentResult { Content = result, ContentType = "application/json", StatusCode = 200 };
                return;
            }
            var send = await next();
            if (send.Result is OkObjectResult contentResult)
                await casheService.SetAsync(key, contentResult.Value, TimeSpan.FromMinutes(_time));
        }
        private string GenerateCacheKey(HttpRequest request)
        {
            var str = new StringBuilder();
            str.Append(request.Path);
            foreach (var (key, value) in request.Query)
                str.Append($"{key}-{value}");
            return str.ToString();
        }
    }
}
