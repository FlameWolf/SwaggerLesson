using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerLesson.Filters
{
	public class TestResultFilter : IAsyncResultFilter
	{
		protected ILogger _logger;

		public TestResultFilter(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.CreateLogger<TestResultFilter>();
		}

		public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			var resultContext = await next();
		}
	}
}