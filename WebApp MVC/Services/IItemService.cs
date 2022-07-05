using WebApp.Models;

namespace WebApp.Services
{
	public interface IItemService
	{
		Task<IEnumerable<Item>> GetItemsAsync();
		Task<Item> GetItemAsync(Guid id);
		Task CreateItemAsync(Item item);
		Task UpdateItemAsync(Item item);
		Task DeleteItemAsync(Guid id);
	}
}
