using Inventory.Core.Entities;

namespace Inventory.Core.Interfaces.Repositories
{
	public interface IStockItemRepository
	{
		Task AddAsync(StockItem stockItem);
		Task<StockItem?> GetByIdAsync(Guid id);
		Task<IEnumerable<StockItem>> GetAllAsync();
	}
}
