using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Features.Commands.CreateStockItem;
using Inventory.Application.Features.Queries.GetStockItemById;

namespace Inventory.Api.Controllers
{
	[Route("api/stock-items")]
	[ApiController]
	public class StockItemsController : ControllerBase
	{
		private readonly GetStockItemByIdQueryHandler _getByIdHandler;
		private readonly CreateStockItemCommandHandler _createStockItemCommandHandler;


		public StockItemsController(
			GetStockItemByIdQueryHandler getByIdHandler,
			CreateStockItemCommandHandler createStockItemCommandHandler)
		{
			_getByIdHandler = getByIdHandler;
			_createStockItemCommandHandler = createStockItemCommandHandler;
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{

			var stockItem = await _getByIdHandler.Handle(new GetStockItemByIdQuery(id));

			if(stockItem == null) return NotFound();
			
			return Ok(stockItem);	//Stef's tab
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateStockItemCommand command, CancellationToken cancellationToken)
		{
			var stockItem = await _createStockItemCommandHandler.Handle(command, cancellationToken);
			
			return CreatedAtAction(nameof(GetById), new { id = stockItem.IdStockItem}, stockItem);
		}
	}
}
