using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwaggerLesson.Models;
using SwaggerLesson.Utility;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace SwaggerLesson.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class JsonPlaceholderController : ControllerBase
	{
		string _baseUri = "https://jsonplaceholder.typicode.com/";
		JsonPlaceholderClient _jsonPlaceholderClient;

		public JsonPlaceholderController(IHttpContextAccessor httpContextAccessor)
		{
			_jsonPlaceholderClient = new JsonPlaceholderClient
			(
				new HttpClient
				{
					BaseAddress = new Uri(_baseUri)
				},
				httpContextAccessor
			);
		}

		[Route("get-all-posts")]
		[HttpGet]
		public async Task<IList<Post>> GetAllPosts()
		{
			return await _jsonPlaceholderClient.GetAllPosts();
		}

		[Route("get-all-comments")]
		[HttpGet]
		public async Task<IList<Comment>> GetAllComments()
		{
			return await _jsonPlaceholderClient.GetAllComments();
		}

		/// <summary>
		/// Creates a new post
		/// </summary>
		/// <param name="request"></param>
		/// <returns>A newly created post</returns>
		[Route("create-post")]
		[HttpPost]
		[SwaggerRequestExample(typeof(Post), typeof(PostExample))]
		public async Task<Post> CreatePost(Post request)
		{
			return await _jsonPlaceholderClient.CreatePost(request);
		}

		/// <summary>
		/// Creates a new comment
		/// </summary>
		/// <param name="request"></param>
		/// <returns>A newly created comment</returns>
		[Route("create-comment")]
		[HttpPost]
		[SwaggerRequestExample(typeof(Comment), typeof(CommentExample))]
		public async Task<Comment> CreateComment(Comment request)
		{
			return await _jsonPlaceholderClient.CreateComment(request);
		}
	}
}