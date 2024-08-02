#region Namespace
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog.Extensions.Logging;
#endregion
namespace pfba.sales.crm.creation
{
	/// <summary>
	/// Represents the entry point of the application.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// The main method that is called when the application starts.
		/// </summary>
		/// <param name="args">The command line arguments.</param>
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		/// <summary>
		/// Creates an instance of the <see cref="IHostBuilder"/> and configures it.
		/// </summary>
		/// <param name="args">The command line arguments.</param>
		/// <returns>The configured <see cref="IHostBuilder"/>.</returns>
		public static IHostBuilder CreateHostBuilder(string[] args) =>
				Host.CreateDefaultBuilder(args)
				.ConfigureLogging((hostingContext, logging) =>
				{
					logging.AddNLog();
				})
				.ConfigureAppConfiguration(options =>
				{
					options.AddJsonFile("Common/StoredProcs.json", optional: false, reloadOnChange: true);
				})
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});

	}
}
