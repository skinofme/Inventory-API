using Microsoft.AspNetCore.Mvc;
using TechShop.Inventory.Application.Features.Commands.Warehouses.CreateWarehouse;

namespace TechShop.Inventory.Api.Controllers
{
	[Route("api/warehouses")]
	[ApiController]
	public class WarehousesController : ControllerBase
	{
		private CreateWarehouseCommandHandler _createWarehouseCommandHandler;
		public WarehousesController(CreateWarehouseCommandHandler createWarehouseCommandHandler)
		{
			_createWarehouseCommandHandler = createWarehouseCommandHandler;
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id) 
		{
			await Task.Delay(777);
			throw new NotImplementedException();
		}


		[HttpPost]
		public async Task<IActionResult> CreateWarehouse([FromBody] CreateWarehouseCommand command, CancellationToken cancellationToken)
		{
			var warehouse = await _createWarehouseCommandHandler.Handle(command, cancellationToken);

			return CreatedAtAction(nameof(GetById), new { id = warehouse.IdWarehouse }, warehouse);
		}
	}
}
