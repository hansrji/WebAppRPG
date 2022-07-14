using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using RPGWebAPI.Controllers;
using RPGWebAPI.Repository;
using RPGWebAPI.Services;
using RPGWebAPI.Settings;
using System.Diagnostics;
using AspNetCore.Identity.Mongo;
using AspNetCore.Identity.Mongo.Model;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RPGWebAPI
{
	public class Startup : IStartup
	{
		protected const int DefaultTimeout = 30;

		protected IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			Debug.WriteLine("Startup configuring.");
			if (env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(options =>
				{
					options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
					options.RoutePrefix = "swagger";
				});
			}
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
			Debug.WriteLine("Startup configuration done.");
		}

		public virtual void ConfigureServices(IServiceCollection services)
		{
			Debug.WriteLine($"{nameof(Startup)} {nameof(ConfigureServices)}");
			var dbSettings = Configuration.GetSection(nameof(MongoDatabaseSettings)).Get<MyMongoDatabaseSettings>();
			var jwtSettings = Configuration.GetSection("jwt").Get<JwtSettings>();

			// Add services to the container.
			services.AddControllers(options =>
			{
				/* Fix for asynchronous controller methods using nameof() and async prefix. */
				options.SuppressAsyncSuffixInActionNames = false;
			});
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo()
				{
					Version = "v1",
					Title = "RPGWebApi",
					Description = "Test"
				});
			});

			BsonSerializer.RegisterSerializer(new GuidSerializer());
			//BsonSerializer.RegisterSerializer(new DateTimeSerializer());

			services.AddIdentityMongoDbProvider<MongoUser, MongoRole>(id =>
			{
				// These are defaults, but should be explicit for futureproofing.
				id.Password.RequiredLength = 6;
				id.Password.RequireNonAlphanumeric = true;
				id.Password.RequireUppercase = true;
				id.Password.RequireLowercase = true;
				id.Password.RequireDigit = true;
				id.User.RequireUniqueEmail = true;
				id.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
			},
			mongo =>
			{
				mongo.ConnectionString = dbSettings.ConnectionString;
			});

			services.AddSingleton<IMongoClient>(provider =>
			{
				return new MongoClient(dbSettings.ConnectionString);
			});

			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(cfg =>
			{
				cfg.RequireHttpsMetadata = false;
				cfg.SaveToken = true;
				cfg.TokenValidationParameters = new TokenValidationParameters
				{
					ValidIssuer = jwtSettings.Issuer,
					ValidAudience = jwtSettings.Issuer,
					IssuerSigningKey = new SymmetricSecurityKey(jwtSettings.KeyAsBytes),
					ClockSkew = TimeSpan.Zero
				}; 
			});

			// Is this better?
			// services.AddScoped<IItemService, ItemService>();
			// services.AddSingleton<IItemService, LocalMemoryStoreService>();
			services.AddSingleton<IItemRepository, DatabaseItemRepository>();
			services.AddSingleton<IStoreService, GeneralStoreService>();
		}
	}
}
