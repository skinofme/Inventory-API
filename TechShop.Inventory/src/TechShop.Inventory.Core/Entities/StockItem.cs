using TechShop.Inventory.Core.Enums.InventoryMovement;
using TechShop.Inventory.Core.Enums.StockReservation;
using TechShop.Inventory.Core.Exceptions.common;
using TechShop.Inventory.Core.Exceptions.StockItem;

namespace TechShop.Inventory.Core.Entities
{

	public class StockItem
	{
		public Guid IdStockItem { get; private set; }

		public Guid IdWarehouse { get; private set; }

		public string Sku { get; private set; }

		// It indicates whether the product is available for sale.
		// This flag is independent of stock quantity.
		public bool IsActive { get; private set; }

		public int QuantityAvailable { get; private set; }

		public int QuantityReserved { get; private set; }

		public int QuantityTotal => QuantityAvailable + QuantityReserved;


		private readonly List<InventoryMovement> _movements = new();
		private readonly List<StockReservation> _reservations = new();

		public IReadOnlyCollection<InventoryMovement> Movements => _movements;
		public IReadOnlyCollection<StockReservation> Reservations => _reservations;



		private StockItem() { }

		#region	FACTORY METHODS
		
		public static StockItem Create(Guid idWarehouse, string sku)
		{

			if (idWarehouse == Guid.Empty) throw new InvalidIdWarehouseException(idWarehouse);
			if (string.IsNullOrWhiteSpace(sku)) throw new InvalidSkuException(sku);

			return new StockItem()
			{
				IdStockItem = Guid.NewGuid(),
				IdWarehouse = idWarehouse,
				Sku = sku,
				IsActive = true,
				QuantityAvailable = 0,
				QuantityReserved = 0
			};

		}

		
		internal static StockItem Rehydrate(Guid idStockItem, Guid idWarehouse, string sku, int quantityAvailable, int quantityReserved, bool isActive)
		{
			if (idStockItem == Guid.Empty) throw new InvalidIdStockItemException(idStockItem);

			ValidateState(idWarehouse, sku, quantityAvailable, quantityReserved);
			return new StockItem()
			{
				IdStockItem = idStockItem,
				IdWarehouse = idWarehouse,
				Sku = sku,
				IsActive = isActive,
				QuantityAvailable = quantityAvailable,
				QuantityReserved = quantityReserved
			};

		}
		
		#endregion	FACTORY METHODS


		#region DOMAIN METHODS

		public void AddStock(int quantity)
		{
			ValidateQuantity(quantity);

			QuantityAvailable += quantity;

			RegisterMovement(this.IdStockItem, MovementType.IN, quantity, "Add Stock");
		}

		public void RemoveStock(int quantity, string reason)
		{
			ValidateQuantity(quantity);

			if (quantity > QuantityTotal) throw new InsufficientStockException(Sku, quantity, QuantityTotal);

			var impact = CalculateMinimalRemoveImpact(quantity);

			QuantityAvailable -= impact.available;
			QuantityReserved -= impact.reserved;

			RegisterMovement(IdStockItem, MovementType.ADJUST, quantity, reason);
		}


		public void ReserveStock(
			int quantity,
			DateTime now,
			TimeSpan duration,
			string reason,
			string referenceId)
		{
			if (!IsActive) throw new InactiveStockItemException(IdStockItem);

			ValidateQuantity(quantity);
			if (quantity > QuantityAvailable) throw new InsufficientStockException(Sku, quantity, QuantityAvailable);

			var reservation = StockReservation.Create(
				IdStockItem,
				quantity,
				now,
				now.Add(duration),
				reason,
				referenceId
			);

			QuantityAvailable -= quantity;
			QuantityReserved += quantity;

			_reservations.Add(reservation);
			
			RegisterMovement(IdStockItem, MovementType.RESERVE, reservation.Quantity, reservation.Reason, reservation.ReferenceId);
		}
		
		public void SellStock(Guid idStockReservation, DateTime now)
		{

			if (!IsActive) throw new InactiveStockItemException(IdStockItem);

			var reservation = _reservations.FirstOrDefault(res => res.IdStockReservation == idStockReservation)
				?? throw new StockReservationNotFoundException(idStockReservation);

			if (reservation.Quantity > QuantityReserved)
				throw new InsufficientStockException(Sku, reservation.Quantity, QuantityReserved);

			reservation.Confirm(now);

			QuantityReserved -= reservation.Quantity;

			RegisterMovement(
				IdStockItem,
				MovementType.OUT,
				reservation.Quantity,
				"Sell",
				reservation.ReferenceId
			);
		}

		public void CancelStockReservation(Guid idStockReservation)
		{

			var reservation = _reservations.FirstOrDefault(res => res.IdStockReservation == idStockReservation)
				?? throw new StockReservationNotFoundException(idStockReservation);

			reservation.Cancel();

			ReleaseReservedStock(reservation.Quantity, "Cancelled reservation", reservation.ReferenceId);
		}

		public void ExpireStockReservations(DateTime now)
		{
			var stockReservations = _reservations.FindAll(
				res => res.Status == ReservationStatus.PENDING
				&& res.ExpiresAt <= now
			);

			if (stockReservations.Count == 0) return;


			foreach (var reservation in stockReservations)
			{
				reservation.Expire(now);

				ReleaseReservedStock(reservation.Quantity, "Expired reservation", reservation.ReferenceId);
			}
		}

		private void ReleaseReservedStock(int quantity, string? reason, string? referenceId)
		{
			// Ensure the stock invariant
			if (quantity > QuantityReserved) throw new InsufficientStockException(Sku, quantity, QuantityReserved);

			// Then release the reserved stock
			QuantityReserved -= quantity;
			QuantityAvailable += quantity;

			RegisterMovement(IdStockItem, MovementType.RELEASE, quantity, reason, referenceId);
		}

		public void Activate()
		{
			if(IsActive) return;
			IsActive = true;
		}
		public void Deactivate()
		{
			if(!IsActive) return;
			IsActive = false;
		}


		#endregion DOMAIN METHODS


		private void RegisterMovement(Guid idStockItem, MovementType movementType, int quantity, string? reason, string? referenceId = null)
		{
			var movement = InventoryMovement.Create(
				idStockItem,
				movementType,
				quantity,
				reason,
				referenceId
			);

			_movements.Add(movement);
		}

		private void ValidateQuantity(int quantity)
		{
			if (quantity <= 0) throw new InvalidQuantityException(quantity);
		}

		private static void ValidateState(Guid idWarehouse, string sku, int quantityAvailable, int quantityReserved)
		{
			if (idWarehouse == Guid.Empty) throw new InvalidIdWarehouseException(idWarehouse);
			if (string.IsNullOrWhiteSpace(sku)) throw new InvalidSkuException(sku);
			if (quantityAvailable < 0) throw new InvalidQuantityException(quantityAvailable);
			if (quantityReserved < 0 ) throw new InvalidQuantityException(quantityReserved);
		}

		
		// Calculates how a stock removal should be distributed between
		// available stock and reserved stock, minimizing the impact on
		// active reservations.
		//
		// Business rule:
		// Available stock is consumed first. Reserved stock is only
		// reduced when available stock is insufficient.
		//
		// Note:
		// Pending reservations are preserved and validated again during
		// SellStock(). A reservation does not guarantee future stock
		// availability after physical inventory adjustments.
		private (int available, int reserved) CalculateMinimalRemoveImpact(int quantity)
		{
			// We ensure the operation is valid
			if (QuantityTotal - quantity < 0) return (0, 0);

			// We get the subtraction with less impact in reservations
			int temp = QuantityAvailable - quantity;
			
			if (temp < 0) return (quantity + temp, Math.Abs(temp));
			else return (quantity, 0);

		}
	}

}
