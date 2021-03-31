using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerLesson.Filters
{
	public class TestFilterFactory : Attribute, IFilterFactory
	{
		public bool IsReusable => false;

		private class InternalResultFilter : IAsyncResultFilter
		{
			private ILogger _logger;

			public InternalResultFilter(ILoggerFactory loggerFactory)
			{
				_logger = loggerFactory.CreateLogger<InternalResultFilter>();
			}

			public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
			{
				context.HttpContext.Response.Headers.Add
				(
					nameof(InternalResultFilter),
					new[] { nameof(TestFilterFactory) }
				);
				await next();
				_logger.LogInformation
				(
					$"From {nameof(InternalResultFilter)} inside {nameof(TestFilterFactory)}"
				);
			}
		}

		public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
		{
			return new InternalResultFilter(serviceProvider.GetService<ILoggerFactory>());
		}
	}
}