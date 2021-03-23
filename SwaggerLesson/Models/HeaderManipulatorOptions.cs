using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerLesson.Models
{
	public class HeaderManipulatorOptions
	{
		public List<string> Headers { get; } = new List<string>();

		public void Add(string header)
		{
			Headers.Add(header);
		}
	}
}