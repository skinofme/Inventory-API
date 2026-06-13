namespace TechShop.Inventory.Application.Features.DTOs
{
	public record StockItemDto(
		Guid IdStockItem,
		Guid IdWarehouse,
		string Sku,
		bool IsActive,
		int QuantityAvailable,
		int QuantityReserved,
		int QuantityTotal
	);
}
