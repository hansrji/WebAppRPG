namespace RPGWebAPI.Models
{
	public record class Store
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public ISet<Item>? Items { get; set; }
	}

	public static class StoreFactory
	{
		public static Store Create(string name, IEnumerable<Item> items)
		{
			var store = new Store()
			{
				Id = Guid.NewGuid(),
				Name = name,
				Items = new HashSet<Item>()
			};
			foreach (var item in items)
			{
				store.Items.Add(item);
			}
			return store;
		}
	}
}
