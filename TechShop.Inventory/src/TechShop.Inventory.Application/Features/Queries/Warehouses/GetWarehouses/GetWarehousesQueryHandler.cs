using Microsoft.EntityFrameworkCore;
using TechShop.Inventory.Application.Features.DTOs;
using TechShop.Inventory.Infrastructure.Persistence.Context;

namespace TechShop.Inventory.Application.Features.Queries.Warehouses.GetWarehouses
{
	public class GetWarehousesQueryHandler
	{
		private readonly TechShopInventoryContext _context;

		public GetWarehousesQueryHandler(TechShopInventoryContext context)
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
