using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerLesson.Filters
{
	public class TestAlwaysRunResultFilter : Attribute, IAsyncAlwaysRunResultFilter
	{
		private readonly ILogger _logger;

		public TestAlwaysRunResultFilter(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.CreateLogger<TestAlwaysRunResultFilter>();
		}

		public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			var jsonResult = context.Result as JsonResult;
			if(jsonResult != null)
			{
				var resultAsJObject = JObject.FromObject(jsonResult.Value);
				resultAsJObject.Add("customFielld", DateTime.Now.ToString());
				context.Result = new ContentResult
				{
					ContentType = "application/json",
					Content = resultAsJObject.ToString()
				};
			}
			var resultContext = await next();
			_logger.LogInformation($"Result from {nameof(TestAlwaysRunResultFilter)} = {resultContext.Result.ToString()}");
		}
	}
}