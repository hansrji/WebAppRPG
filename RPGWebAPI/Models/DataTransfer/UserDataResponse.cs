namespace RPGWebAPI.Models.DataTransfer
{
	public record UserDataResponse
	{
		public string? Name { get; set; }
		public string? LastName { get; set; }
		public string? City { get; set; }
		public string? Email { get; set; }
	}
}
