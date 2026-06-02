namespace TechShop.Inventory.Core.Exceptions.StockReservation
{
	public class InvalidIdStockItemException : DomainException
	{
		public InvalidIdStockItemException(Guid idStockItem)
			:base($"Invalid requested idStockItem:{idStockItem}, cannot be empty")
		{}
	}
}
