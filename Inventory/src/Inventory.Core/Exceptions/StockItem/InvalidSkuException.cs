namespace Inventory.Core.Exceptions.StockItem
{
	public class InvalidSkuException : DomainException
	{
		public InvalidSkuException(string sku) 
			:base($"The requested SKU: {sku} cannot be a white space or be empty.")
		{ 
		}
	}
}
