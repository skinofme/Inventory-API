namespace Inventory.Infrastructure.Exceptions.StockItemRepository
{
	public class StockItemNotFoundException : InfrastructureException
	{
		public StockItemNotFoundException(Guid idStockItem) 
			:base($"The source requested: {idStockItem} cannot be found.")
		{ 
		}
	}
}
