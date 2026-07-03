namespace Inventory.Infrastructure.Persistence.Models;

public partial class InventoryMovementEntity
{
    public int IdInventoryMovement { get; set; }

    public Guid IdStockItem { get; set; }

    public string MovementType { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Reason { get; set; }

    public string? ReferenceId { get; set; }

    public virtual StockItemEntity IdStockItemNavigation { get; set; } = null!;
}
