namespace RPGWebAPI.Settings
{
	public class MyMongoDatabaseSettings
	{
		public string? Hostname { get; set; }

		public string ConnectionString
		{
			get
			{
				return $"{Hostname}";
			}
		}
	}
}
