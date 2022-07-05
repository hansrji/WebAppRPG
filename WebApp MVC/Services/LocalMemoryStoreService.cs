using WebApp.Models;

namespace WebApp.Services
{
	public class LocalMemoryStoreService : IItemService
	{
		protected IEnumerable<Item> Items { get; }
		protected Lazy<Item> InvalidItem { get; }
			= new Lazy<Item>(() => ItemFactory.Create("Invalid", 0));

		public LocalMemoryStoreService()
		{
			Items = new List<Item>()
			{
				ItemFactory.Create("Basic Sword", 10),
				ItemFactory.Create("Basic Shield", 12)
			};
		}

		public Item? GetItem(Guid id)
			=> Items.SingleOrDefault(item => item.Id == id);

		public void CreateItem(Item item)
		{
			if (Items is IList<Item> items)
				items.Add(item);
		}

		public void UpdateItem(Item item)
		{
			
		}

		public void DeleteItem(Guid id)
		{
		}

		public async Task<IEnumerable<Item>> GetItemsAsync()
		{
			return await Task.FromResult(Items);
		}

		public async Task<Item> GetItemAsync(Guid id)
		{
			return await Task.FromResult(Items.SingleOrDefault(item => item.Id == id) ?? InvalidItem.Value);
		}

		public async Task CreateItemAsync(Item item)
		{
			if (Items is IList<Item> items)
				items.Add(item);
			await Task.CompletedTask;
		}

		public async Task UpdateItemAsync(Item item)
		{
			if (Items is List<Item> items)
			{
				int index = items.FindIndex(e => e.Id == item.Id);
				items[index] = item;
			}
			await Task.CompletedTask;
		}

		public async Task DeleteItemAsync(Guid id)
		{
			if (Items is List<Item> items)
			{
				var index = items.FindIndex(e => e.Id == id);
				items.RemoveAt(index);
			}
			await Task.CompletedTask;
		}
	}
}
