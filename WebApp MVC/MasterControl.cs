using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System.Diagnostics;
using WebApp.Services;
using WebApp.Settings;

namespace WebApp
{
	public class MasterControl : IHostControl
	{
		protected const int DefaultTimeout = 30;

		protected IConfiguration Configuration { get; }

		public MasterControl(IConfiguration configuration)
		{
			Debug.WriteLine("MasterControl ctor");
			Configuration = configuration;
		}

		public virtual void ConfigureServices(IServiceCollection services)
		{
			Debug.WriteLine($"{nameof(MasterControl)} {nameof(ConfigureServices)}");
			// Add services to the container.
			services.AddControllersWithViews(options =>
			{
				options.SuppressAsyncSuffixInActionNames = false;
			});
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo()
				{
					Version = "v1",
					Title = "WebAPP1",
					Description = "Test"
				});
			});

			BsonSerializer.RegisterSerializer(new GuidSerializer());
			//BsonSerializer.RegisterSerializer(new DateTimeSerializer());

			services.AddSingleton<IMongoClient>(provider =>
			{
				var settings = Configuration.GetSection(nameof(MongoDatabaseSettings)).Get<MyMongoDatabaseSettings>();
				Debug.WriteLine(settings.ConnectionString);
				return new MongoClient(settings.ConnectionString);
			});
			// Is this better?
			// services.AddScoped<IItemService, ItemService>();
			// services.AddSingleton<IItemService, LocalMemoryStoreService>();
			services.AddSingleton<IItemService, DatabaseStoreService>();
		}

		public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			Debug.WriteLine("MasterControl Configuring");
			if (env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(options =>
				{
					options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
					options.RoutePrefix = string.Empty;
				});
			}
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
