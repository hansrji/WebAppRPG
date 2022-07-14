using MongoDB.Bson;
using MongoDB.Driver;
using RPGWebAPI.Models;

namespace RPGWebAPI.Repository
{
	public class DatabaseItemRepository : IItemRepository
	{
		protected const string DatabaseName = "rpg";
		protected const string CollectionName = "items";

		protected readonly IMongoCollection<Item> items;

		protected readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

		public DatabaseItemRepository(IMongoClient client)
		{
			IMongoDatabase database = client.GetDatabase(DatabaseName);
			items = database.GetCollection<Item>(CollectionName);
		}

		public async Task CreateItemAsync(Item item)
		{
			await items.InsertOneAsync(item);
		}

		public async Task DeleteItemAsync(Guid id)
		{
			var filter = filterBuilder.Eq(item => item.Id, id);
			await items.DeleteOneAsync(filter);
		}

		public async Task<Item?> GetItemAsync(Guid id)
		{
			var filter = filterBuilder.Eq(item => item.Id, id);
			return await items.Find(filter).SingleOrDefaultAsync();
		}

		public async Task<IEnumerable<Item>> GetItemsAsync()
		{
			return await items.Find(new BsonDocument()).ToListAsync();
		}

		public async Task UpdateItemAsync(Item item)
		{
			var filter = filterBuilder.Eq(item => item.Id, item.Id);
			await items.ReplaceOneAsync(filter, item);
		}
	}
}
