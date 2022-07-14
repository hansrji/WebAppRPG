using RPGWebAPI.Models;

namespace RPGWebAPI.Repository
{
	public interface IItemRepository
	{
		Task<IEnumerable<Item>> GetItemsAsync();
		Task<Item?> GetItemAsync(Guid id);
		Task CreateItemAsync(Item item);
		Task UpdateItemAsync(Item item);
		Task DeleteItemAsync(Guid id);
	}
}
