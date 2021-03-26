using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerLesson.Models
{
	public class Post
	{
		public int UserId { set; get; }
		public int Id { set; get; }
		public string Title { set; get; }
		public string Body { set; get; }
	}

	public class PostExample : IExamplesProvider<Post>
	{
		public Post GetExamples()
		{
			return new Post
			{
				UserId = 1,
				Id = 1,
				Title = "Post title",
				Body = "Post content"
			};
		}
	}
}