using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.DataTransferObjects
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
