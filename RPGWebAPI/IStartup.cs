namespace RPGWebAPI
{
	public interface IStartup
	{
		void Configure(IApplicationBuilder app, IWebHostEnvironment env);
		void ConfigureServices(IServiceCollection services);
	}
}
