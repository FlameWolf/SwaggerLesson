using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerLesson.Filters
{
	public class TestActionTypeFilter : IAsyncActionFilter
	{
		private ILogger _logger;
		private string _name;
		private string _value;

		public TestActionTypeFilter(ILoggerFactory loggerFactory, string name, string value)
		{
			_logger = loggerFactory.CreateLogger<TestActionTypeFilter>();
			_name = name;
			_value = value;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			context.HttpContext.Response.Headers.Add(_name, new[] { _value });
			var resultContext = await next();
			_logger.LogInformation($"Result from {nameof(TestActionTypeFilter)} = {resultContext.Result.ToString()}");
		}
	}
}