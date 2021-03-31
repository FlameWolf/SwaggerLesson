using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerLesson.Filters
{
	public class TestResultFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.ResultFilterAttribute
	{
		private readonly string _key;
		private readonly string _value;

		public TestResultFilterAttribute(string key, string value)
		{
			_key = key;
			_value = value;
		}

		public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			context.HttpContext.Response.Headers.Add(_key, new[] { _value });
			await base.OnResultExecutionAsync(context, next);
		}
	}
}