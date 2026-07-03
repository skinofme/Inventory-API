namespace Inventory.Core.Exceptions.StockReservation
{
	public class StockReservationExpiredException : DomainException
	{
		public DateTime ExpiresAt { get; }
		public DateTime CurrentTime { get; }

		public StockReservationExpiredException(DateTime expiresAt, DateTime currentTime)
			:base($"Reservation is already expired at {expiresAt}. Current time: {currentTime}.")
		{
			ExpiresAt = expiresAt;
			CurrentTime = currentTime;
		}
	}
}
