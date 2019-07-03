using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Eventures.Filters
{
	public class AdminActivityFilter : ActionFilterAttribute
	{
		private readonly ILogger<AdminActivityFilter> logger;

		public AdminActivityFilter(ILogger<AdminActivityFilter> logger)
		{
			this.logger = logger;
		}

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			await next();
			string username = context.HttpContext.User.Identity.Name;
			string eventName = context.HttpContext.Request.Form["Name"];
			string end = context.HttpContext.Request.Form["End"];
			string start = context.HttpContext.Request.Form["Start"];

			logger.LogInformation($"{DateTime.Now} Administrator {username} create event {eventName} ({end} / {start})");
		}
	}
}
