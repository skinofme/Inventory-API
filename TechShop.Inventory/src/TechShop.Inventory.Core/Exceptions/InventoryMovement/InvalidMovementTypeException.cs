using TechShop.Inventory.Core.Enums.InventoryMovement;

namespace TechShop.Inventory.Core.Exceptions.InventoryMovement
{
	public class InvalidMovementTypeException : DomainException
	{
		public InvalidMovementTypeException(MovementType movementType)
			:base($"Invalid Movement type: {movementType}.")
		{
		}
	}
}
