using System.ComponentModel.DataAnnotations;

namespace WebApp.DataTransferObjects
{
	public record UpdateItemDto
	{
		[Required]
		public Guid Id { get; init; }
		public string? Name { get; init; }
		public decimal Price { get; init; }
	}
}
