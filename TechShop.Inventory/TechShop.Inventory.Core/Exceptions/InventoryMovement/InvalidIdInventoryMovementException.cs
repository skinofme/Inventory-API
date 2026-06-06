namespace TechShop.Inventory.Core.Exceptions.InventoryMovement
{
	public class InvalidIdInventoryMovementException : DomainException
	{
		public InvalidIdInventoryMovementException(int idInventoryMovement)
			: base($"Invalid IdInventoryMovement: {idInventoryMovement} cannot be less than or equal to zero.")
		{			
		}
	}
}
