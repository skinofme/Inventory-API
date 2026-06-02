using System;
using System.Collections.Generic;

namespace TechShop.Inventory.Infrastructure.Persistence.Models;

public partial class StockReservationEntity
{
    public Guid IdStockReservation { get; set; }

    public Guid IdStockItem { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public string Status { get; set; } = null!;

    public string? Reason { get; set; }

    public string? ReferenceId { get; set; }

    public virtual StockItemEntity IdStockItemNavigation { get; set; } = null!;
}
