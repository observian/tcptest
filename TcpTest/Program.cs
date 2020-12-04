using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TelnetTest
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateHostBuilder(string[] args)
		{
			//dynamic port variable used in CloudRun
			var port = Environment.GetEnvironmentVariable("PORT");
            
			//debugging statement in case the port didn't get passed correctly
			Console.WriteLine($"env PORT is {port ?? ("not found")}");

			return WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.UseKestrel()
				.ConfigureAppConfiguration((hostingContext, config) =>
				{
					config.AddEnvironmentVariables();
				})
				.ConfigureKestrel((context, options) => { options.Listen(IPAddress.IPv6Any, Convert.ToInt32(port ?? "8080")); });
		}
	}
}