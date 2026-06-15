using TechShop.Inventory.Core.Interfaces;
using TechShop.Inventory.Infrastructure.Persistence.Context;

namespace TechShop.Inventory.Infrastructure.Persistence
{
	public class UnitOfWork : IUnitOfWork
	{

		private readonly TechShopInventoryContext _context;
		public UnitOfWork(TechShopInventoryContext context) => _context = context;

		public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
		{
			return await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
