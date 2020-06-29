using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TelnetTest.Models;

namespace TelnetTest.Controllers
{
	[ApiController]
	[Route("/")]
	public class RootController : ControllerBase
	{
		[HttpPost]
		public IActionResult Index([FromBody] Request obj)
		{
			var response = new Response();
			foreach (var thing in obj.Resources)
			{
				var resourceResult = new ResourceResult(thing);
				try
				{
					var tcpClient = new TcpClient();
					tcpClient.SendTimeout = obj.SendTimeout;
					tcpClient.ReceiveTimeout = obj.ReceiveTimeout;
					tcpClient.Connect(thing.IpAddress, thing.Port);
					if (tcpClient.Connected)
					{
						resourceResult.ResultText = "Successful";
					}
				}
				catch (Exception e)
				{
					resourceResult.ResultText = e.Message;
					Console.WriteLine(e);
				}
				response.ResourceResults.Add(resourceResult);
			}

			return Ok(JsonConvert.SerializeObject(response));
		}
	}
}