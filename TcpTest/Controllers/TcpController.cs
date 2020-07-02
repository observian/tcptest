using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TelnetTest.Models;

namespace TelnetTest.Controllers
{
	[ApiController]
	[Route("[controller]")]
	
	public class TcpController : ControllerBase
	{
		private object _lockObj;
		/// <summary>
		/// Simply add a list of IP Addresses and the ports, and the API will test them concurrently.  IP Addresses on the same port will be
		/// tested serially.
		/// </summary>
		/// <param name="req">Request containing the list of IP Addresses, ports and the desired timeout.</param>
		/// <returns></returns>
		[HttpPost] 
		public IActionResult Index([FromBody] Request req)
		{
			var uniquePorts = req.Resources
				.GroupBy(t => t.Port)
				.Select(q => q.Key);

			var dict = uniquePorts
				.ToDictionary(port => port, port => 
					req.Resources.Where(x => x.Port == port).ToList());
			
			var response = new Response();


			Parallel.ForEach(uniquePorts, (currentPort) =>
			{
				foreach (var request in dict[currentPort])
				{
					
					var resourceResult = new ResourceResult(request);
					try
					{
						var client = new TcpClient();
						var result = client.BeginConnect(request.IpAddress, request.Port, null, null);
						var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(req.Timeout));
						resourceResult.ResultText = !success ? $"Timed out after {req.Timeout} seconds" : "Successful";
					}
					catch (Exception e)
					{
						resourceResult.ResultText = e.Message;
					}
					lock (response)
					{
						response.ResourceResults.Add(resourceResult);
					}
				}
			});

			return Ok(JsonConvert.SerializeObject(response));
		}
	}
}