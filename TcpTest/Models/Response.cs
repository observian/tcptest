using System.Collections.Generic;

namespace TelnetTest.Models
{
	public class Response
	{
		public List<ResourceResult> ResourceResults { get; set; }

		public Response()
		{
			ResourceResults = new List<ResourceResult>();
		}
	}
}