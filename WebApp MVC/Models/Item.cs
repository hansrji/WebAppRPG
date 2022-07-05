using WebApp.DataTransferObjects;

namespace WebApp.Models
{
	public record Item
	{
		public const int MinimumPrice = 1;
		public const int MaximumPrice = 10000;

		public Guid Id { get; set; }
		public string? Name { get; set; }
		public decimal Price { get; set; }
	}

	public static class ItemExtensions
	{
		public static ItemDto AsDto(this Item item)
		{
			return new ItemDto()
			{
				Id = item.Id,
				Name = item.Name,
				Price = item.Price
			};
		}
	}

	public class ItemFactory
	{
		public static Item Create(string name, decimal price)
		{
			return new Item()
			{
				Id = Guid.NewGuid(),
				Name = name,
				Price = price
			};
		}
	}
}
