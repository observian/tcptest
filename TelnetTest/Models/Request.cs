using System.Collections.Generic;

namespace TelnetTest.Models
{
	public class Request
	{
		public List<Resource> Resources { get; set; }
		public int SendTimeout { get; set; }
		public int ReceiveTimeout { get; set; }
	}
}