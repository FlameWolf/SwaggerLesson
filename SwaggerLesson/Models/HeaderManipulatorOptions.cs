using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerLesson.Models
{
	public class HeaderManipulatorOptions
	{
		private readonly List<string> _headers;

		public ImmutableArray<string> Headers => _headers.ToImmutableArray();

		public HeaderManipulatorOptions()
		{
			_headers = new List<string>();
		}

		public void Add(string header)
		{
			_headers.Add(header);
		}
	}
}