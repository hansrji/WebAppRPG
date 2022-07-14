using System.ComponentModel.DataAnnotations;

namespace RPGWebAPI.Models.DataTransfer
{
	public record UserRequestDto
	{
		[Required(ErrorMessage = "Name is required.")]
		public string? Name { get; set; }

		[Required(ErrorMessage = "Email is required.")]
		[DataType(DataType.EmailAddress)]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Phone number is required.")]
		public string? PhoneNumber { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		[DataType(DataType.Password)]
		public string? Password { get; set; }

		[Required(ErrorMessage = "Password confirmation is required.")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Passwords do not match.")]
		public string? ConfirmPassword { get; set; }
	}
}
