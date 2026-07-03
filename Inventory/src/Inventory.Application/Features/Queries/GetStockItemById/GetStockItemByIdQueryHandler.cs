using Microsoft.EntityFrameworkCore;
using Inventory.Application.Features.DTOs;
using Inventory.Infrastructure.Persistence.Context;

namespace Inventory.Application.Features.Queries.GetStockItemById
{
	public class GetStockItemByIdQueryHandler
	{
		private readonly InventoryContext _context;

		public GetStockItemByIdQueryHandler(InventoryContext context) => _context = context;

		public async Task<StockItemDto?> Handle(GetStockItemByIdQuery query)
		{
			var stockItem = await _context.StockItems
				.AsNoTracking()
				.Where(item => item.IdStockItem == query.Id)
				.Select(item =>
					new StockItemDto(
						item.IdStockItem,
						item.IdWarehouse,
						item.Sku,
						item.IsActive,
						item.QuantityAvailable,
						item.QuantityReserved,
						item.QuantityAvailable + item.QuantityReserved
					)
				)
				.SingleOrDefaultAsync();

			if (stockItem == null) return null;

			return stockItem;
		}

	}
}
