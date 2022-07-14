namespace RPGWebAPI.Models
{
	public record class Item
	{
		public const int MinimumPrice = 1;
		public const int MaximumPrice = 10000;

		public Guid Id { get; set; }
		public string? Name { get; set; }
		public decimal Price { get; set; }
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
