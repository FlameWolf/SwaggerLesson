using Microsoft.AspNetCore.Http;
using SwaggerLesson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SwaggerLesson.Utility
{
	public class JsonPlaceholderClient : BaseHttpClient
	{
		string postsEndpoint = "posts";
		string commendsEndpoint = "posts/1/comments";

		public JsonPlaceholderClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient, httpContextAccessor)
		{
		}

		public async Task<IList<Post>> GetAllPosts()
		{
			return await base.Get<IList<Post>>(postsEndpoint);
		}

		public async Task<IList<Comment>> GetAllComments()
		{
			return await base.Get<IList<Comment>>(commendsEndpoint);
		}

		public async Task<Post> CreatePost(Post post)
		{
			return await base.Post<Post>(postsEndpoint, post);
		}

		public async Task<Comment> CreateComment(Comment comment)
		{
			return await base.Post<Comment>(commendsEndpoint, comment);
		}
	}
}