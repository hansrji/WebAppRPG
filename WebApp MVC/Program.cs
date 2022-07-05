using Microsoft.OpenApi.Models;
using System.Diagnostics;
using WebApp.Services;

namespace WebApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Debug.WriteLine("Program main init");
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					Debug.WriteLine("Configuring web host defaults");
					webBuilder.UseStartup<MasterControl>();
				});
	}
}
