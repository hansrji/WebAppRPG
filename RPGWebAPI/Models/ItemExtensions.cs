using RPGWebAPI.Models.DataTransfer;

namespace RPGWebAPI.Models
{
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
}
