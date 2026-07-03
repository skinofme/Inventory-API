namespace Inventory.Application.Features.Commands.CreateStockItem
{
	public record CreateStockItemCommand(Guid IdWarehouse, string Sku);
}
