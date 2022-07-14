using System.ComponentModel.DataAnnotations;

namespace RPGWebAPI.Models.DataTransfer
{
	public record ItemDto
	{
		[Required(ErrorMessage = "Item id is required.")]
		public Guid Id { get; init; }

		[Required(ErrorMessage = "Name is required.")]
		public string? Name { get; init; }

		[Required(ErrorMessage = "Price is required.")]
		[Range(Item.MinimumPrice, Item.MaximumPrice)]
		public decimal Price { get; init; }
	}
}
