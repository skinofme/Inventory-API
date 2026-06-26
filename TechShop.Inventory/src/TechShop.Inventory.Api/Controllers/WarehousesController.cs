using Microsoft.AspNetCore.Mvc;
using TechShop.Inventory.Application.Features.Commands.Warehouses.CreateWarehouse;
using TechShop.Inventory.Application.Features.Queries.Warehouses.GetWarehouseById;
using TechShop.Inventory.Application.Features.Queries.Warehouses.GetWarehouses;

namespace TechShop.Inventory.Api.Controllers
{
	[Route("api/warehouses")]
	[ApiController]
	public class WarehousesController : ControllerBase
	{
		private readonly CreateWarehouseCommandHandler _createWarehouseCommandHandler;
		private readonly GetWarehouseByIdQueryHandler _getWarehouseByIdQueryHandler;
		private readonly GetWarehousesQueryHandler _getWarehousesQueryHandler;
		public WarehousesController(
			CreateWarehouseCommandHandler createWarehouseCommandHandler,
			GetWarehouseByIdQueryHandler getWarehouseByIdQueryHandler,
			GetWarehousesQueryHandler getWarehousesQueryHandler
		)
		{
			_createWarehouseCommandHandler = createWarehouseCommandHandler;
			_getWarehouseByIdQueryHandler = getWarehouseByIdQueryHandler;
			_getWarehousesQueryHandler = getWarehousesQueryHandler;
		}



		[HttpGet]
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
		{
			var warehouses = await _getWarehousesQueryHandler.Handle(new GetWarehousesQuery(), cancellationToken);

			return Ok(warehouses);
		}


		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken) 
		{
			var warehouse = await _getWarehouseByIdQueryHandler.Handle(new GetWarehouseByIdQuery(id), cancellationToken);

			if (warehouse == null) return NotFound();

			return Ok(warehouse);
		}


		[HttpPost]
		public async Task<IActionResult> CreateWarehouse([FromBody] CreateWarehouseCommand command, CancellationToken cancellationToken)
		{
			var warehouse = await _createWarehouseCommandHandler.Handle(command, cancellationToken);

			return CreatedAtAction(nameof(GetById), new { id = warehouse.IdWarehouse }, warehouse);
		}
	}
}
