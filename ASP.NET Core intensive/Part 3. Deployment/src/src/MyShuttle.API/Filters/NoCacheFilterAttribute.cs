namespace MyShuttle.API
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;

    internal class NoCacheFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.HttpContext.Response.StatusCode == 200 &&
				!actionExecutedContext.HttpContext.Response.Headers.ContainsKey("CacheControl"))
            {
                actionExecutedContext.HttpContext.Response.Headers["CacheControl"] = "no-cache";
                actionExecutedContext.HttpContext.Response.Headers["Pragma"] = "no-cache";

                if (!actionExecutedContext.HttpContext.Response.Headers.ContainsKey("Expires"))
                {
                    actionExecutedContext.HttpContext.Response.Headers["Expires"] = DateTimeOffset.UtcNow.AddDays(-1).ToString();
                }
            }
        }
    }
}