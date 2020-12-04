using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace TelnetTest.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DiagnosticController : ControllerBase
	{

		private readonly IConfiguration _config;
		
		
		public DiagnosticController(IConfiguration config)
		{
			_config = config;
		}

		/// <summary>
		/// returns basic info about where the pod is running
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IActionResult Get()
		{
			dynamic json = new 
			{
				hostName = _config.GetValue<string>("HOST_NAME") ?? "not found"
				, podIp = _config.GetValue<string>("POD_IP") ?? "not found"
				, hostIp = _config.GetValue<string>("HOST_IP") ?? "not found"
			};
			
			return Ok(JsonConvert.SerializeObject(json));
		}
	}
}