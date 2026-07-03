namespace Inventory.Application.Exceptions
{
	public sealed class WarehouseNotFoundException : Exception
	{
		public WarehouseNotFoundException(Guid idWarehouse)
			:base($"Warehouse with id '{idWarehouse}' was not found.")
		{			
		}
	}
}
