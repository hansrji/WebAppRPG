using RPGWebAPI.Models.Identity;

namespace RPGWebAPI.Models.DataTransfer
{
	public class LoginEntity
	{
		public string Email { get; internal set; }
		public string Password { get; internal set; }
	}
}