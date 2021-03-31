using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SwaggerLesson.Filters
{
	public class TestExceptionFilter : IAsyncExceptionFilter
	{
		private readonly ILogger _logger;
		private readonly IWebHostEnvironment _environment;
		private readonly IModelMetadataProvider _provider;

		public TestExceptionFilter(ILoggerFactory loggerFactory, IWebHostEnvironment environment, IModelMetadataProvider provider)
		{
			_logger = loggerFactory.CreateLogger<TestExceptionFilter>();
			_environment = environment;
			_provider = provider;
		}

		public async Task OnExceptionAsync(ExceptionContext context)
		{
			var result = new ObjectResult(context.Exception.Message);
			result.ContentTypes = new MediaTypeCollection
			{
				new MediaTypeHeaderValue("text/plain")
			};
			result.StatusCode = (int)HttpStatusCode.InternalServerError;
			context.ExceptionHandled = true;
			context.Result = result;
		}
	}
}