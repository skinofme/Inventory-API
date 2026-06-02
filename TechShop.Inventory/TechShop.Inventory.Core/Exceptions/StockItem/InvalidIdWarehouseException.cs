namespace TechShop.Inventory.Core.Exceptions.StockItem
{
	internal class InvalidIdWarehouseException : DomainException
	{
		public InvalidIdWarehouseException(Guid idWarehouse)
			: base($"Invalid requested IdWarehouse: {idWarehouse}, cannot be empty")
		{ }
	}
}
