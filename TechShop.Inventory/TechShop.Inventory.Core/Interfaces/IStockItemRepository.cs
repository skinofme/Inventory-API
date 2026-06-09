using TechShop.Inventory.Core.Entities;

namespace TechShop.Inventory.Core.Interfaces
{
	public interface IStockItemRepository
	{
		Task AddAsync(StockItem stockItem);
		Task<StockItem> GetByIdAsync(Guid id);
		Task<IEnumerable<StockItem>> GetAllAsync();
	}
}
