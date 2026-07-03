using Inventory.Core.Enums.StockReservation;
using Inventory.Core.Exceptions.common;
using Inventory.Core.Exceptions.StockReservation;

namespace Inventory.Core.Entities
{
	public class StockReservation
	{
		public Guid IdStockReservation { get; private set; }

		public Guid IdStockItem { get; private set; }

		public int Quantity { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public DateTime ExpiresAt { get; private set; }

		public ReservationStatus Status { get; private set; }

		public string? Reason { get; private set; }

		public string? ReferenceId { get; private set; }


		private StockReservation() { }


		#region FACTORY METHODS

		internal static StockReservation Create(
			Guid idStockItem,
			int quantity,
			DateTime createdAt,
			DateTime expiresAt,
			string? reason,
			string? referenceId)
		{

			ValidateState(idStockItem, quantity, createdAt, expiresAt);

			return new StockReservation() {

				IdStockReservation = Guid.NewGuid(),
				IdStockItem = idStockItem,
				Quantity = quantity,
				CreatedAt = createdAt,
				ExpiresAt = expiresAt,
				Status = ReservationStatus.PENDING,
				Reason = reason,
				ReferenceId = referenceId
			};
		}

		internal static StockReservation Rehydrate(
			Guid idStockReservation,
			Guid idStockItem,
			int quantity,
			DateTime createdAt,
			DateTime expiresAt,
			ReservationStatus status,
			string? reason,
			string? referenceId)
		{
			if (idStockReservation == Guid.Empty) throw new InvalidIdStockReservationException(idStockReservation);
			ValidateState(idStockItem, quantity, createdAt, expiresAt);

			return new StockReservation()
			{

				IdStockReservation = idStockReservation,
				IdStockItem = idStockItem,
				Quantity = quantity,
				CreatedAt = createdAt,
				ExpiresAt = expiresAt,
				Status = status,
				Reason = reason,
				ReferenceId = referenceId
			};
		}

		#endregion FACTORY METHODS


		#region DOMAIN METHODS

		
		internal void Confirm(DateTime now)
		{
			if (Status != ReservationStatus.PENDING) 
				throw new InvalidStockReservationStateException(Status, "Confirm");

			if (ExpiresAt <= now) throw new StockReservationExpiredException(ExpiresAt, now);

			Status = ReservationStatus.CONFIRMED;
		}

		
		internal void Cancel()
		{
			if (Status != ReservationStatus.PENDING) 
				throw new InvalidStockReservationStateException(Status, "Cancel"); // Only pending reservatin can expire

			Status = ReservationStatus.CANCELLED;
		}

		
		internal void Expire(DateTime now)
		{
			if (Status != ReservationStatus.PENDING)
				throw new InvalidStockReservationStateException(Status, "Expire"); // Only pending reservation can expire

			if (ExpiresAt > now )
				throw new StockReservationNotExpiredException(ExpiresAt, now); // It cannot expire before

			Status = ReservationStatus.EXPIRED;
		}

		#endregion DOMAIN METHODS


		private static void ValidateState(Guid idStockItem, int quantity, DateTime createdAt, DateTime expiresAt)
		{
			if (idStockItem == Guid.Empty) throw new InvalidIdStockItemException(idStockItem);
			if (quantity <= 0) throw new InvalidQuantityException(quantity);
			if (expiresAt <= createdAt) throw new InvalidExpirationDateException(expiresAt, createdAt);

		}

	}
}
