namespace TechShop.Inventory.Core.Exceptions.StockReservation
{
	public class StockReservationNotExpiredException : DomainException
	{
		public DateTime ExpiredAt { get; }
		public DateTime CurrentTime { get; }

		public StockReservationNotExpiredException(DateTime expiredAt, DateTime currentTime)
			:base($"Reservation cannot be expired before {expiredAt}. Current time: {currentTime}.")
		{
			ExpiredAt = expiredAt;
			CurrentTime = currentTime;
		}
	}
}
