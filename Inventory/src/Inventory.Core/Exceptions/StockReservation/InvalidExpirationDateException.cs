namespace Inventory.Core.Exceptions.StockReservation
{
	public class InvalidExpirationDateException : DomainException
	{
		public InvalidExpirationDateException(DateTime expiresAt, DateTime createdAt)
			:base($"Invalid requested expiration date, expiresAt: {expiresAt}, cannot be less than createdAt: {createdAt}.")
		{ 
		}
	}
}
