namespace Inventory.Core.Exceptions.StockItem
{
	public class InactiveStockItemException : DomainException
	{
		public InactiveStockItemException(Guid idStockItem)
			:base($"The inactive Stock Item: {idStockItem} cannot be sold or reserved.")
		{
		}
	}
}
