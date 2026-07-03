using Inventory.Core.Interfaces;
using Inventory.Infrastructure.Persistence.Context;

namespace Inventory.Infrastructure.Persistence
{
	public class UnitOfWork : IUnitOfWork
	{

		private readonly InventoryContext _context;
		public UnitOfWork(InventoryContext context) => _context = context;

		public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
		{
			return await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
