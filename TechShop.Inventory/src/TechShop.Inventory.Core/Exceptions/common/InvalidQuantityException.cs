namespace TechShop.Inventory.Core.Exceptions.common
{
	public class InvalidQuantityException : Exception
	{
		public InvalidQuantityException(int invalidQuantity) 
			:base($"Invalid requested quantity: {invalidQuantity}, quantity must be positive.")
		{
		}
	}
}
