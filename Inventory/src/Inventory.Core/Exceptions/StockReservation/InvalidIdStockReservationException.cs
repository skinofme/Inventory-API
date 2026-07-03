namespace Inventory.Core.Exceptions.StockReservation
{
	public class InvalidIdStockReservationException : DomainException
	{
		public InvalidIdStockReservationException(Guid idStockReservation)
			:base($"Invalid requested IdstockReservation: {idStockReservation}, cannot be empty.")
		{
		}
	}
}
