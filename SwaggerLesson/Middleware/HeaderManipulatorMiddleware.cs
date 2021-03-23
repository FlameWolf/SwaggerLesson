using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
		private readonly ImmutableList<string> _headersToRemove;

		public HeaderManipulatorMiddleware(RequestDelegate next, ImmutableList<string> headersToRemove)
		{
			_next = next;
			_headersToRemove = headersToRemove;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			httpContext.Response.OnStarting(() =>
			{
				_headersToRemove.ForEach(headerToRemove =>
				{
					if (httpContext.Response.Headers.ContainsKey(headerToRemove))
					{
						httpContext.Response.Headers.Remove(headerToRemove);
					}
				});
				return Task.FromResult(0);
			});
			await _next(httpContext);
		}
	}

	public static class HeaderManipulatorMiddlewareExtensions
	{
		public static IApplicationBuilder UseHeaderManipulatorMiddleware(this IApplicationBuilder builder, params string[] headersToRemove)
		{
			return builder.UseMiddleware<HeaderManipulatorMiddleware>(headersToRemove.ToImmutableList());
		}
	}
}