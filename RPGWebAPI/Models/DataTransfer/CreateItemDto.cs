using System.ComponentModel.DataAnnotations;

namespace RPGWebAPI.Models.DataTransfer
{
	public record CreateItemDto
	{
		[Required]
		public string? Name { get; init; }
		[Required]
		[Range(Item.MinimumPrice, Item.MaximumPrice)]
		public decimal Price { get; init; }
	}
}
