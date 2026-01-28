using TechShop.Inventory.Core.Enums.InventoryMovement;
using TechShop.Inventory.Core.Enums.StockReservation;
using TechShop.Inventory.Core.Exceptions.common;
using TechShop.Inventory.Core.Exceptions.StockItem;

namespace TechShop.Inventory.Core.Entities
{

	public class StockItem
	{
		public int IdStockItem { get; private set; }

		public int IdWarehouse { get; private set; }

		public string Sku { get; private set; }


		public int QuantityAvailable { get; private set; }

		public int QuantityReserved { get; private set; }

		public int QuantityTotal => QuantityAvailable + QuantityReserved;


		private readonly List<InventoryMovement> _movements = new();
		private readonly List<StockReservation> _reservations = new();

		public IReadOnlyCollection<InventoryMovement> Movements => _movements;
		public IReadOnlyCollection<StockReservation> Reservations => _reservations;


		#region	CONSTRUCTORS

		private StockItem() { }

		// Constructor to create a new entity
		public static StockItem Create(string sku, int idWarehouse)
		{
			var stockItem = new StockItem();

			if (string.IsNullOrWhiteSpace(sku)) throw new InvalidSkuException(sku);
			if (idWarehouse <= 0) throw new InvalidIdWarehouseException(idWarehouse);

			stockItem.Sku = sku;
			stockItem.IdWarehouse = idWarehouse;
			stockItem.QuantityAvailable = 0;
			stockItem.QuantityReserved = 0;
			
			return stockItem;
		}

		// Constructor to rehydrate a entity
		internal StockItem Rehydrate(int idStockItem, int idWarehouse, string sku, int quantityAvailable, int quantityReserved)
		{
			var stockItem = new StockItem();

			stockItem.IdStockItem = idStockItem;
			stockItem.IdWarehouse = idWarehouse;
			stockItem.Sku = sku;
			stockItem.QuantityAvailable = quantityAvailable;
			stockItem.QuantityReserved = quantityReserved;

			return stockItem;
		}
		#endregion


		#region DOMAIN METHODS

		public void AddStock(int quantity)
		{
			validateQuantity(quantity);

			QuantityAvailable += quantity;

			RegisterMovement(this.IdStockItem, MovementType.IN, quantity, "Add Stock");
		}

		public void RemoveStock(int quantity, string reason)
		{
			validateQuantity(quantity);
			if (quantity > QuantityAvailable) throw new InsufficientStockException(Sku, quantity, QuantityAvailable);

			QuantityAvailable -= quantity;

			RegisterMovement(this.IdStockItem, MovementType.ADJUST, quantity, reason);
		}

		public void SellStock(int idStockReservation, DateTime now)
		{
			var reservation = _reservations.FirstOrDefault(res => res.IdStockReservation == idStockReservation)
				?? throw new StockReservationNotFoundException(idStockReservation);

			reservation.Confirm(now);

			validateQuantity(reservation.Quantity);
			if (reservation.Quantity > QuantityReserved) throw new InsufficientStockException(Sku, reservation.Quantity, QuantityReserved);

			QuantityReserved -= reservation.Quantity;

			RegisterMovement(
				IdStockItem,
				MovementType.OUT,
				reservation.Quantity,
				"Sell",
				reservation.ReferenceId
			);
		}

		public void ReserveStock(
			int quantity,
			DateTime now,
			TimeSpan duration,
			string reason,
			string referenceId)
		{

			validateQuantity(quantity);
			if (quantity > QuantityAvailable) throw new InsufficientStockException(Sku, quantity, QuantityAvailable);

			QuantityAvailable -= quantity;
			QuantityReserved += quantity;

			var reservation = new StockReservation(
				IdStockItem,
				quantity,
				now, 
				now.Add(duration),
				reason, 
				referenceId
			);

			_reservations.Add(reservation);
			
			RegisterMovement(IdStockItem, MovementType.RESERVE, reservation.Quantity, reservation.Reason, reservation.ReferenceId);
		}
		
		public void CancelStockReservation(int idStockReservation)
		{

			var reservation = _reservations.FirstOrDefault(res => res.IdStockReservation == idStockReservation);

			if (reservation == null) throw new StockReservationNotFoundException(idStockReservation);

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


		#endregion DOMAIN METHODS

		private void ReleaseReservedStock(int quantity, string reason, string referenceId)
		{
			// Ensure the stock invariant
			if (quantity > QuantityReserved) throw new InsufficientStockException(Sku, quantity, QuantityReserved);

			// Then release the reserved stock
			QuantityReserved -= quantity;
			QuantityAvailable += quantity;

			RegisterMovement(IdStockItem, MovementType.RELEASE, quantity, reason, referenceId);
		}

		private void RegisterMovement(int idStockItem, MovementType movementType, int quantity, string reason, string referenceId = null)
		{
			var movement = new InventoryMovement(
				idStockItem,
				movementType,
				quantity,
				reason,
				referenceId
			);

			_movements.Add(movement);
		}

		private void validateQuantity(int quantity)
		{
			if (quantity <= 0) throw new InvalidQuantityException(quantity);
		}

	}

}
