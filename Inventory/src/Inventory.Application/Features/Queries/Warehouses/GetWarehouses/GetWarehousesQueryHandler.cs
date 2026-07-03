using Microsoft.EntityFrameworkCore;
using Inventory.Application.Features.DTOs;
using Inventory.Infrastructure.Persistence.Context;

namespace Inventory.Application.Features.Queries.Warehouses.GetWarehouses
{
	public class GetWarehousesQueryHandler
	{
		private readonly InventoryContext _context;

		public GetWarehousesQueryHandler(InventoryContext context)
		{
			_context = context;
		}


		public async Task<List<WarehouseDto>> Handle(GetWarehousesQuery query, CancellationToken cancellationToken)
		{
			var warehouses = await _context.Warehouses
				.AsNoTracking()
				.Select(item => 
					new WarehouseDto(
						item.IdWarehouse,
						item.Code,
						item.Name,
						item.Location,
						item.IsActive)
				).ToListAsync(cancellationToken);

			return warehouses;
		}
	}
}
