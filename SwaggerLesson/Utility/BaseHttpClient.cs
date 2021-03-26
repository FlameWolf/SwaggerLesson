using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SwaggerLesson.Utility
{
	public class BaseHttpClient
	{
		private readonly HttpClient _httpClient;
		private readonly IHttpContextAccessor _httpContextAccessor;

		protected BaseHttpClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
		{
			_httpClient = httpClient;
			_httpContextAccessor = httpContextAccessor;
		}

		private static HttpRequestMessage CreateRequest(HttpMethod httpMethod, string uri, object content = null)
		{
			var request = new HttpRequestMessage(httpMethod, uri);
			if (content != null)
			{
				request.Content = new StringContent
				(
					JsonSerializer.Serialize(content),
					Encoding.UTF8,
					"application/json"
				);
			}
			return request;
		}

		private async Task<T> ExecuteRequest<T>(HttpRequestMessage request)
		{
			var response = await _httpClient.SendAsync
			(
				request,
				HttpCompletionOption.ResponseHeadersRead
			)
			.ConfigureAwait(false);
			foreach
			(
				var header in response.Headers.Where
				(
					x =>
					(
						string.Compare
						(
							x.Key,
							"transfer-encoding",
							StringComparison.InvariantCultureIgnoreCase
						) != 0
					)
				)
			)
			{
				_httpContextAccessor.HttpContext.Response.Headers.Add
				(
					header.Key,
					header.Value.Aggregate
					(
						(accumulator, value) => $"{accumulator}, {value}"
					)
				);
			}
			var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			response.EnsureSuccessStatusCode();
			return
			(
				string.IsNullOrEmpty(responseContent) ?
				default :
				JsonSerializer.Deserialize<T>(responseContent)
			);
		}

		protected async Task<T> Get<T>(string uri)
		{
			return await ExecuteRequest<T>
			(
				CreateRequest(HttpMethod.Get, uri)
			);
		}

		protected async Task<T> Post<T>(string uri, object content)
		{
			return await ExecuteRequest<T>
			(
				CreateRequest(HttpMethod.Post, uri, content)
			);
		}
	}
}