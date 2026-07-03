namespace Inventory.Infrastructure.Exceptions
{
	public abstract class InfrastructureException : Exception
	{
		public InfrastructureException() 
		{
		}

		public InfrastructureException(string message) : base(message)
		{
		}

		public InfrastructureException(string message, Exception innerException) : base(message, innerException) 
		{
		}
	}
}
