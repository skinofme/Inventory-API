namespace TechShop.Inventory.Core.Exceptions.common
{
	public class InvalidIdStockItemException : DomainException
	{
		public InvalidIdStockItemException(Guid idStockItem)
			:base($"Invalid requested idStockItem:{idStockItem}, cannot be empty.")
		{
		}
	}
}
