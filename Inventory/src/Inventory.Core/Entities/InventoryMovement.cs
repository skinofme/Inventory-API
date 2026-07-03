using Inventory.Core.Enums.InventoryMovement;
using Inventory.Core.Exceptions.common;
using Inventory.Core.Exceptions.InventoryMovement;

namespace Inventory.Core.Entities
{
	public class InventoryMovement
	{
		public int IdInventoryMovement { get; private set; }

		public Guid IdStockItem {  get; private set; }

		public MovementType MovementType { get; private set; }

		public int Quantity { get; private set; }

		public string? Reason {  get; private set; }

		public string? ReferenceId { get; private set; }

		
		private InventoryMovement() { }

		internal static InventoryMovement Create(Guid idStockItem, MovementType movementType, int quantity, string? reason, string? referenceId)
		{

			ValidateState(idStockItem, movementType, quantity);

			return new InventoryMovement()
			{
				IdStockItem = idStockItem,
				MovementType = movementType,
				Quantity = quantity,
				Reason = reason,
				ReferenceId = referenceId

			};

		}

		internal static InventoryMovement Rehydrate(int idInventoryMovement, Guid idStockItem, MovementType movementType, int quantity, string? reason, string? referenceId)
		{
			ValidateState(idStockItem, movementType, quantity);

			if (idInventoryMovement <= 0) throw new InvalidIdInventoryMovementException(idInventoryMovement);

			return new InventoryMovement()
			{
				IdInventoryMovement = idInventoryMovement,
				IdStockItem = idStockItem,
				MovementType = movementType,
				Quantity = quantity,
				Reason = reason,
				ReferenceId = referenceId
			};

		}

		private static void ValidateState(Guid idStockItem, MovementType movementType, int quantity)
		{
			if (idStockItem == Guid.Empty) throw new InvalidIdStockItemException(idStockItem);

			if (!Enum.IsDefined(movementType)) throw new InvalidMovementTypeException(movementType);

			if (quantity <= 0) throw new InvalidQuantityException(quantity);
		}
	}
}
