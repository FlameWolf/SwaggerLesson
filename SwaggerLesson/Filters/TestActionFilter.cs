using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerLesson.Filters
{
	public class TestActionFilter : IAsyncActionFilter
	{
		private ILogger _logger;

		public TestActionFilter(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.CreateLogger<TestActionFilter>();
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			_logger.LogInformation("Log before executing action");
			var resultContext = await next();
			_logger.LogInformation("Log after executing action");
		}
	}
}