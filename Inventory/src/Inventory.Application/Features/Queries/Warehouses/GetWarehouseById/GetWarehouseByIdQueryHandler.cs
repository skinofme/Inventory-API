using Microsoft.EntityFrameworkCore;
using Inventory.Application.Features.DTOs;
using Inventory.Infrastructure.Persistence.Context;

namespace Inventory.Application.Features.Queries.Warehouses.GetWarehouseById
{
	public class GetWarehouseByIdQueryHandler
	{
		private readonly InventoryContext _context;

		public GetWarehouseByIdQueryHandler(InventoryContext context) => _context = context;

		public async Task<WarehouseDto?> Handle(GetWarehouseByIdQuery query, CancellationToken cancellationToken)
		{
			var warehouse = await _context.Warehouses
				.AsNoTracking()
				.Where(wh => wh.IdWarehouse == query.IdWarehouse)
				.Select(item =>
					new WarehouseDto(
						item.IdWarehouse,
						item.Code,
						item.Name,
						item.Location,
						item.IsActive
					)
				)
				.SingleOrDefaultAsync(cancellationToken);

			if (warehouse == null) return null;

			return warehouse;

		}

	}
}
