using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerLesson.Models
{
	public class Comment
	{
		public int PostId { set; get; }
		public int Id { set; get; }
		public string Name { set; get; }
		public string Email { set; get; }
	}

	public class CommentExample : IExamplesProvider<Comment>
	{
		public Comment GetExamples()
		{
			return new Comment
			{
				PostId = 1,
				Id = 1,
				Name = "Jane Doe",
				Email = "jane.doe@server.net"
			};
		}
	}
}