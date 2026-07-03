using Microsoft.EntityFrameworkCore;
using Inventory.Core.Entities;
using Inventory.Core.Interfaces.Repositories;
using Inventory.Infrastructure.Persistence.Context;
using Inventory.Infrastructure.Persistence.Models;

namespace Inventory.Infrastructure.Persistence.Repositories
{
	public class WarehouseRepository : IWarehouseRepository
	{
		private readonly InventoryContext _context;

		public WarehouseRepository(InventoryContext context) => _context = context;


		public async Task<Warehouse?> GetByIdAsync(Guid warehouseId, CancellationToken cancellationToken)
		{
			var warehouse = await _context.Warehouses
				.Where(wh => wh.IdWarehouse == warehouseId)
				.Select(wh => Warehouse.Rehydrate(
						wh.IdWarehouse,
						wh.Code,
						wh.Name,
						wh.Location,
						wh.IsActive
					)
				)
				.FirstOrDefaultAsync(cancellationToken);

			return warehouse;
		}

		public async Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken)
		{
			var efEntity = new WarehouseEntity()
			{
				IdWarehouse = warehouse.IdWarehouse,
				Code = warehouse.Code,
				Name = warehouse.Name,
				Location = warehouse.Location,
				IsActive = warehouse.IsActive,
			};
			
			await _context.Warehouses.AddAsync(efEntity, cancellationToken);
		}

		
		public async Task UpdateAsync(Warehouse warehouse, CancellationToken cancellationToken)
		{
			var entity = await _context.Warehouses
				.SingleAsync(wh => wh.IdWarehouse == warehouse.IdWarehouse, cancellationToken);

			entity.Code = warehouse.Code;
			entity.Name = warehouse.Name;
			entity.Location = warehouse.Location;
			entity.IsActive = warehouse.IsActive;
		}

		public async Task<bool> IsActiveAsync(Guid warehouseId, CancellationToken cancellationToken)
		{
			return await _context.Warehouses.AnyAsync(
				wh => wh.IdWarehouse == warehouseId && wh.IsActive, cancellationToken);
		}
	}
}
