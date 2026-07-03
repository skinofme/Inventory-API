using Inventory.Core.Entities;

namespace Inventory.Core.Interfaces.Repositories
{
	public interface IWarehouseRepository
	{
		Task<Warehouse?> GetByIdAsync(Guid warehouseId, CancellationToken cancellationToken = default);
		Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken = default);
		Task UpdateAsync(Warehouse warehouse, CancellationToken cancellationToken = default);
		Task<bool> IsActiveAsync(Guid warehouseId, CancellationToken cancellationToken = default);
	}
}