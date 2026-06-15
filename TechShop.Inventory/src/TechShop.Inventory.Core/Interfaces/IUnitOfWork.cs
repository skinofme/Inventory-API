namespace TechShop.Inventory.Core.Interfaces
{
	public interface IUnitOfWork
	{
		Task<int> SaveAsync(CancellationToken cancellationToken);
	}
}
