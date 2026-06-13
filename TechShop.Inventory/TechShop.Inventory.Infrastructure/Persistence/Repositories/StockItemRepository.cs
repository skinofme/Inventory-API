using Microsoft.EntityFrameworkCore;
using TechShop.Inventory.Core.Entities;
using TechShop.Inventory.Core.Interfaces.Repositories;
using TechShop.Inventory.Infrastructure.Persistence.Context;
using TechShop.Inventory.Infrastructure.Persistence.Models;

namespace TechShop.Inventory.Infrastructure.Persistence.Repositories
{
	public class StockItemRepository : IStockItemRepository
	{
		private readonly TechShopInventoryContext _context;

		public StockItemRepository(TechShopInventoryContext context)
		{
			_context = context;
		}

		public async Task AddAsync(StockItem stockItem)
		{
			var stockItemEntity = new StockItemEntity()
			{
				IdStockItem = stockItem.IdStockItem,
				IdWarehouse = stockItem.IdWarehouse,
				Sku = stockItem.Sku,
				QuantityAvailable = stockItem.QuantityAvailable,
				QuantityReserved = stockItem.QuantityReserved,
				QuantityTotal = stockItem.QuantityTotal,
				IsActive = stockItem.IsActive,
			};

			await _context.StockItems.AddAsync(stockItemEntity);
		}
		public async Task<StockItem?> GetByIdAsync(Guid id)
		{
			var stockItemEntity = await _context.StockItems
				.AsNoTracking()
				.SingleOrDefaultAsync(item => item.IdStockItem == id);

			if (stockItemEntity == null) return null;

			return StockItem.Rehydrate(
				stockItemEntity.IdStockItem,
				stockItemEntity.IdWarehouse,
				stockItemEntity.Sku,
				stockItemEntity.QuantityAvailable,
				stockItemEntity.QuantityReserved,
				stockItemEntity.IsActive
			);
		}

		public Task<IEnumerable<StockItem>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

	}
}
