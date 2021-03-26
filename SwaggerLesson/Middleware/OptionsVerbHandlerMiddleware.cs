using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SwaggerLesson.Middleware
{
	public class OptionsVerbHandlerMiddleware
	{
		private readonly RequestDelegate _next;

		public OptionsVerbHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			if (httpContext.Request.Method == HttpMethod.Options.Method)
			{
				httpContext.Response.Clear();
				httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
				httpContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { (string)httpContext.Request.Headers["Origin"] });
				httpContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Origin, X-Requested-With, Content-Type, Accept" });
				httpContext.Response.Headers.Add("Access-Control-Allow-Methods", new[] { GetAllowedVerbs(httpContext.GetEndpoint()) });
				httpContext.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
				await httpContext.Response.CompleteAsync();
			}
			await _next(httpContext);
		}

		private string GetAllowedVerbs(Endpoint endpoint)
		{
			var allowedVerbs = string.Empty;
			if (endpoint != null)
			{
				var metadata = endpoint.Metadata;
				var constraint = metadata.Where
				(
					x => x is HttpMethodActionConstraint
				)
				.FirstOrDefault() as HttpMethodActionConstraint;
				if (constraint != null)
				{
					allowedVerbs =
					(
						constraint.HttpMethods.Aggregate
						(
							(accumulator, value) => $"{accumulator}, {value}"
						)
					);
				}
				if (allowedVerbs == string.Empty)
				{
					var target = endpoint.RequestDelegate.Target;
					var allowField = target.GetType().GetField("allow");
					allowedVerbs =
					(
						allowField != null ?
						allowField.GetValue(target).ToString() :
						string.Empty
					);
				}
			}
			return allowedVerbs;
		}
	}

	public static class OptionsVerbHandlerMiddlewareExtensions
	{
		public static IApplicationBuilder UseOptionsVerbHandlerMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<OptionsVerbHandlerMiddleware>();
		}
	}
}