namespace Inventory.Core.Exceptions.common
{
	public class InvalidNameException : DomainException
	{
		public InvalidNameException(string name)
			:base($"The requested name: {name}, cannot be a white space or be empty.")
		{			
		}
	}
}
