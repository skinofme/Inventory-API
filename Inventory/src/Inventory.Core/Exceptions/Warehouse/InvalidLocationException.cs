namespace Inventory.Core.Exceptions.Warehouse
{
	public class InvalidLocationException : DomainException
	{
		public InvalidLocationException(string location)
			:base($"The requested location: {location}, cannot be a white space or be empty.")
		{
		}
	}
}
