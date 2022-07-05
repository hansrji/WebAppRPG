using Microsoft.AspNetCore.Mvc;
using WebApp.DataTransferObjects;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
	[Route("items")]
	[ApiController]
	public class ItemController : ControllerBase
	{
		protected IItemService ItemService { get; init; }

		public ItemController(IItemService itemService)
		{
			ItemService = itemService;
		}

		[HttpGet]
		public async Task<IEnumerable<ItemDto>> GetItemsAsync() => (await ItemService.GetItemsAsync()).Select(i => i.AsDto());

		[HttpGet("{id}")]
		public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
		{
			var item = await ItemService.GetItemAsync(id);
			if (item == null) return NotFound();
			return item.AsDto();
		}

		[HttpPost]
		public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
		{
			var item = new Item()
			{
				Id = Guid.NewGuid(),
				Name = itemDto.Name,
				Price = itemDto.Price
			};
			await ItemService.CreateItemAsync(item);
			return CreatedAtAction(nameof(GetItemAsync), new { item.Id }, item.AsDto());
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
		{
			var item = await ItemService.GetItemAsync(id);
			if (item == null)
			{
				return NotFound();
			}
			var updatedItem = item with
			{
				Name = itemDto.Name,
				Price = itemDto.Price
			};
			await ItemService.UpdateItemAsync(updatedItem);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteItemAsync(Guid id)
		{
			var item = await ItemService.GetItemAsync(id);
			if (item == null)
			{
				return NotFound();
			}
			await ItemService.DeleteItemAsync(id);
			return NoContent();
		}
	}
}
