using TechShop.Inventory.Core.Entities;

namespace TechShop.Inventory.Core.Interfaces.Repositories
{
	public interface IWarehouseRepository
	{
		Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken = default);
		Task<bool> IsActiveAsync(Guid warehouseId, CancellationToken cancellationToken = default);
	}
}
