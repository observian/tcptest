using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace TelnetTest.Models
{
	public class ResourceResult : Resource
	{
		public string ResultText { get; set; }
		
		public ResourceResult(Resource resource)
		{
			IpAddress = resource.IpAddress;
			Port = resource.Port;
		}
		
	}
}