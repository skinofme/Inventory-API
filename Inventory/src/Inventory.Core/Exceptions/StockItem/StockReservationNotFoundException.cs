namespace Inventory.Core.Exceptions.StockItem
{
	public class StockReservationNotFoundException : DomainException
	{
		public StockReservationNotFoundException(Guid idStockReservation)
			:base($"Stock reservation not found. IdStockReservation: {idStockReservation}.")
		{
		}
	}
}
