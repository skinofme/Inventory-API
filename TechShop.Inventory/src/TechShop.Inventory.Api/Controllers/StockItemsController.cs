using Microsoft.AspNetCore.Mvc;
using TechShop.Inventory.Application.Features.Queries.GetStockItemById;

namespace TechShop.Inventory.Api.Controllers
{
	[Route("api/stock-items")]
	[ApiController]
	public class StockItemsController : ControllerBase
	{
		private readonly GetStockItemByIdQueryHandler _getByIdHandler;

		public StockItemsController(
			GetStockItemByIdQueryHandler getByIdHandler)
		{
			_getByIdHandler = getByIdHandler;
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{

			var stockItem = await _getByIdHandler.Handle(new GetStockItemByIdQuery(id));

			if(stockItem == null) return NotFound();
			
			return Ok(stockItem);
		}
	}
}
