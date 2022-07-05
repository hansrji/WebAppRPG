namespace WebApp
{
	public interface IHostControl
	{
		void Configure(IApplicationBuilder app, IWebHostEnvironment env);
		void ConfigureServices(IServiceCollection services);
	}
}