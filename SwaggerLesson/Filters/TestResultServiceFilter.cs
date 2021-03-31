using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerLesson.Filters
{
	public class TestResultServiceFilter: IAsyncResultFilter
	{
		private ILogger _logger;

		public TestResultServiceFilter(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.CreateLogger<TestResultServiceFilter>();
		}

		public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			context.HttpContext.Response.Headers.Add
			(
				"OnResultExecutionAsync: Before",
				new[] { "Result is being executed" }
			);
			var result = await next();
			_logger.LogInformation($"Result from {nameof(TestResultServiceFilter)} = {result.Result.ToString()}");
		}
	}
}