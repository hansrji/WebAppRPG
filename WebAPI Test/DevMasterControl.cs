using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WebApp.Services;

namespace WebApp
{
	public class DevMasterControl : MasterControl
	{
		public DevMasterControl(IConfiguration configuration) : base(configuration)
		{

		}

		public override void ConfigureServices(IServiceCollection services)
		{
			// Add services to the container.
			services.AddControllersWithViews();
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo()
				{
					Version = "v1",
					Title = "WebAPP1",
					Description = "Test"
				});
			});
			// Is this better?
			// services.AddScoped<IItemService, ItemService>();
			services.AddSingleton<IItemService, LocalMemoryStoreService>();
		}
	}
}
