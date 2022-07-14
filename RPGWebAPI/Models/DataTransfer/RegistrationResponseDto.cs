using System.ComponentModel.DataAnnotations;

namespace RPGWebAPI.Models.DataTransfer
{
	public record RegistrationResponseDto
	{
		public IEnumerable<string>? Errors { get; set; }

		[Required(ErrorMessage = "Success indicator required.")]
		public bool IsRegistrationSuccessful { get; set; }
	}
}
