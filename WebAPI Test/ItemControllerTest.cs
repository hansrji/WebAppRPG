using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApp.Controllers;
using WebApp.Models;
using WebApp.Services;

namespace WebAPI_Test
{
	[TestClass]
	public class ItemControllerTest
	{
		[TestMethod]
		public async Task GetItemAsync_NullItem_ReturnNotFound()
		{
			var itemService = new Mock<IItemService>();
			itemService.Setup(service => service.GetItemAsync(It.IsAny<Guid>()))
				.ReturnsAsync((Item?)null);
			var controller = new ItemController(itemService.Object);

			var result = await controller.GetItemAsync(Guid.NewGuid());

			Assert.IsTrue(result.Result is NotFoundResult);
		}
	}
}
