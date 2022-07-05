namespace WebApp.Settings
{
	public class MyMongoDatabaseSettings
	{
		public string Host { get; set; }
		public int Port { get; set; }
		public string User { get; set; }
		public string Password { get; set; }

		public string ConnectionString
		{
			get
			{
				var login = /*$"{User}:{Password}@"*/ "";
				return $"mongodb://{login}{Host}:{Port}";
			}
		}
	}
}
