using RPGWebAPI.Helpers;

namespace RPGWebAPI.Models.DataTransfer
{
	public record LoginResponse
	{
		public TokenResult<string> Token { get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
	}
}
