using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Features.Commands.Warehouses.ActivateWarehouse;
using Inventory.Application.Features.Commands.Warehouses.CreateWarehouse;
using Inventory.Application.Features.Commands.Warehouses.DeactiveWarehouse;
using Inventory.Application.Features.Queries.Warehouses.GetWarehouseById;
using Inventory.Application.Features.Queries.Warehouses.GetWarehouses;

namespace Inventory.Api.Controllers
{
	[Route("api/warehouses")]
	[ApiController]
	public class WarehousesController : ControllerBase
	{
		private readonly CreateWarehouseCommandHandler _createWarehouseCommandHandler;
		private readonly GetWarehouseByIdQueryHandler _getWarehouseByIdQueryHandler;
		private readonly GetWarehousesQueryHandler _getWarehousesQueryHandler;
		private readonly DeactivateWarehouseCommandHandler _deactivateWarehouseCommandHandler;
		private readonly ActivateWarehouseCommandHandler _activateWarehouseCommandHandler;

		public WarehousesController(
			CreateWarehouseCommandHandler createWarehouseCommandHandler,
			GetWarehouseByIdQueryHandler getWarehouseByIdQueryHandler,
			GetWarehousesQueryHandler getWarehousesQueryHandler,
			DeactivateWarehouseCommandHandler deactivateWarehouseCommandHandler,
			ActivateWarehouseCommandHandler activateWarehouseCommandHandler
		)
		{
			_createWarehouseCommandHandler = createWarehouseCommandHandler;
			_getWarehouseByIdQueryHandler = getWarehouseByIdQueryHandler;
			_getWarehousesQueryHandler = getWarehousesQueryHandler;
			_deactivateWarehouseCommandHandler = deactivateWarehouseCommandHandler;
			_activateWarehouseCommandHandler = activateWarehouseCommandHandler;
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
		public async Task<IActionResult> Create([FromBody] CreateWarehouseCommand command, CancellationToken cancellationToken)
		{
			var warehouse = await _createWarehouseCommandHandler.Handle(command, cancellationToken);

			return CreatedAtAction(nameof(GetById), new { id = warehouse.IdWarehouse }, warehouse);
		}


		[HttpPatch("{id:guid}/deactivate")]
		public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken)
		{
			await _deactivateWarehouseCommandHandler.Handle(new DeactivateWarehouseCommand(id), cancellationToken);
			
			return NoContent();
		}

		[HttpPatch("{id:guid}/activate")]
		public async Task<IActionResult> Activate(Guid id, CancellationToken cancellationToken)
		{
			await _activateWarehouseCommandHandler.Handle(new ActivateWarehouseCommand(id), cancellationToken);

			return NoContent();
		}
	}
}
