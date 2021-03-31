using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerLesson.Filters
{
	public class TestShortCircuitResourceFilterAttribute : Attribute, IAsyncResourceFilter
	{
		public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
		{
			context.Result = new ContentResult
			{
				StatusCode = 200,
				ContentType = "text/plain",
				Content = "Filter short-curcuited request"
			};
		}
	}
}