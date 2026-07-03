namespace Inventory.Application.Features.DTOs
{
	public record WarehouseDto(
		Guid IdWarehouse,
		string Code,
		string Name,
		string Location,
		bool IsActive 
	);
}