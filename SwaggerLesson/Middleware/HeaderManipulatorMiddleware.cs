using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SwaggerLesson.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerLesson.Middleware
{
	public class HeaderManipulatorMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly HeaderManipulatorOptions _headerManipulatorOptions;

		public HeaderManipulatorMiddleware(RequestDelegate next, HeaderManipulatorOptions headerManipulatorOptions)
		{
			_next = next;
			_headerManipulatorOptions = headerManipulatorOptions;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			httpContext.Response.OnStarting(() =>
			{
				_headerManipulatorOptions.Headers.ToList().ForEach(headerToRemove =>
				{
					if (httpContext.Response.Headers.ContainsKey(headerToRemove))
					{
						httpContext.Response.Headers.Remove(headerToRemove);
					}
				});
				return Task.CompletedTask;
			});
			await _next(httpContext);
		}
	}

	public static class HeaderManipulatorMiddlewareExtensions
	{
		private static HeaderManipulatorOptions _options = new HeaderManipulatorOptions();

		public static IApplicationBuilder UseHeaderManipulatorMiddleware(this IApplicationBuilder builder, Action<HeaderManipulatorOptions> action)
		{
			action.Invoke(_options);
			return builder.UseMiddleware<HeaderManipulatorMiddleware>(_options);
		}
	}
}