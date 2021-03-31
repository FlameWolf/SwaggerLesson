using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SwaggerLesson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerLesson.Filters
{
	public class TestActionFilterAttribute: ActionFilterAttribute
	{
		private readonly ILogger<TestActionFilterAttribute> _logger;
		private readonly PositionOptions _options;

		public TestActionFilterAttribute(ILogger<TestActionFilterAttribute> logger, IOptions<PositionOptions> options)
		{
			_logger = logger;
			_options = options.Value;
		}

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			_logger.LogInformation("Before action");
			await base.OnActionExecutionAsync(context, next);
			_logger.LogInformation("After action");
		}

		public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			_logger.LogInformation("Before result");
			context.HttpContext.Response.Headers.Add(_options.Title, new[] {  _options.Name });
			await base.OnResultExecutionAsync(context, next);
			_logger.LogInformation("After result");
		}
	}
}