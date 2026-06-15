namespace TechShop.Inventory.Core.Exceptions.common
{
	public class InvalidIdWarehouseException : DomainException
	{
		public InvalidIdWarehouseException(Guid idWarehouse)
			:base($"The requested Warehouse id: {idWarehouse}, cannot be empty.")
		{
		}
	}
}
