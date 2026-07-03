namespace Inventory.Core.Exceptions.Warehouse
{
	public class InvalidWarehouseCodeException : DomainException
	{
		public InvalidWarehouseCodeException(string code) 
			:base($"Requested code: {code}, cannot be white space or empty.")
		{
		}
	}
}
