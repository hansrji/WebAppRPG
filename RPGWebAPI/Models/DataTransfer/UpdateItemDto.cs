using System.ComponentModel.DataAnnotations;

namespace RPGWebAPI.Models.DataTransfer
{
	public record UpdateItemDto
	{
		[Required(ErrorMessage = "Item id is required.")]
		public Guid Id { get; init; }

		public string? Name { get; init; }

		[Range(Item.MinimumPrice, Item.MaximumPrice)]
		public decimal Price { get; init; }
	}
}
