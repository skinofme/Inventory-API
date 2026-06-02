using System.Linq.Expressions;
using TechShop.Inventory.Core.Enums.StockReservation;
using TechShop.Inventory.Core.Exceptions.common;
using TechShop.Inventory.Core.Exceptions.StockReservation;

namespace TechShop.Inventory.Core.Entities
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

		protected StockReservation() { }

		// constructor to create a new entity
		public StockReservation(
			Guid idStockItem,
			int quantity,
			DateTime createdAt,
			DateTime expiresAt,
			string reason,
			string referenceId)
		{
			ValidateConstructor(idStockItem, quantity, createdAt, expiresAt, reason, referenceId);
			IdStockReservation = Guid.NewGuid();
			IdStockItem = idStockItem;
			Quantity = quantity;
			CreatedAt = createdAt;
			ExpiresAt = expiresAt;
			Status = ReservationStatus.PENDING;
			Reason = reason;
			ReferenceId = referenceId;
		}

		// constructor to rehydrate a entity
		internal StockReservation(
			Guid idStockReservation,
			Guid idStockItem,
			int quantity,
			DateTime createdAt,
			DateTime expiresAt,
			ReservationStatus status,
			string reason,
			string referenceId)
		{
			if (idStockReservation == Guid.Empty) throw new InvalidIdStockReservationException(idStockReservation);
			ValidateConstructor(idStockItem, quantity, createdAt, expiresAt, reason, referenceId);

			IdStockReservation = idStockReservation;
			IdStockItem = idStockItem;
			Quantity = quantity;
			CreatedAt = createdAt;
			ExpiresAt = expiresAt;
			Status = status;
			Reason = reason;
			ReferenceId = referenceId;
		}

		// CONFIRMED
		public void Confirm(DateTime now)
		{
			if (Status == ReservationStatus.CONFIRMED) return; // Idempotency

			if (Status != ReservationStatus.PENDING) 
				throw new InvalidStockReservationStateException(Status, "Confirm");

			if (ExpiresAt <= now) throw new StockReservationExpiredException(ExpiresAt, now);

			Status = ReservationStatus.CONFIRMED;
		}

		// CANCELLED
		public void Cancel()
		{
			if (Status == ReservationStatus.CANCELLED || Status == ReservationStatus.EXPIRED) // Idempotency
				return;

			if (Status != ReservationStatus.PENDING) 
				throw new InvalidStockReservationStateException(Status, "Cancel");

			Status = ReservationStatus.CANCELLED;
		}

		// EXPIRED
		public void Expire(DateTime now)
		{
			if (Status == ReservationStatus.EXPIRED || Status == ReservationStatus.CANCELLED) return; // Idempotency

			if (Status != ReservationStatus.PENDING)
				throw new InvalidStockReservationStateException(Status, "Expire"); // Only pending reservation can be expired

			if (ExpiresAt > now )
				throw new StockReservationNotExpiredException(ExpiresAt, now); // It cannot be expire before

			Status = ReservationStatus.EXPIRED;
		}

		private void ValidateConstructor(Guid idStockItem, int quantity, DateTime createdAt, DateTime expiresAt, string reason, string referenceId)
		{
			if (idStockItem == Guid.Empty) throw new InvalidIdStockItemException(idStockItem);
			if (quantity <= 0) throw new InvalidQuantityException(quantity);
			if (expiresAt <= createdAt) throw new InvalidExpirationDateException(expiresAt, createdAt);
			if (string.IsNullOrWhiteSpace(reason)) throw new InvalidReasonException(reason);
			if (string.IsNullOrWhiteSpace(referenceId)) throw new InvalidReferenceIdException(referenceId);
		}

	}
}
