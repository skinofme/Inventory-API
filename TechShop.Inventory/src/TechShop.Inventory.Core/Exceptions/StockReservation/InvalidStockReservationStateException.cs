using TechShop.Inventory.Core.Enums.StockReservation;

namespace TechShop.Inventory.Core.Exceptions.StockReservation
{
	public class InvalidStockReservationStateException : DomainException
	{
		public ReservationStatus CurrentStatus { get; }
		public string Operation {  get; }

		public InvalidStockReservationStateException(ReservationStatus currentStatus, string operation)
			:base($"Cannot {operation} reservation in state {currentStatus}.")
		{
			CurrentStatus = currentStatus;
			Operation = operation;
		}
	}
}
