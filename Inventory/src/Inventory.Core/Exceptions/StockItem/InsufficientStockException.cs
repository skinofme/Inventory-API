namespace Inventory.Core.Exceptions.StockItem
{
	public class InsufficientStockException : DomainException
	{
		public InsufficientStockException(string sku, int requestedQuantity, int quantityAvailable)
			:base($"Requested quantity: {requestedQuantity}, exceeds available quantity: {quantityAvailable}, for product with SKU: {sku}.")
		{
		}
	}
}
