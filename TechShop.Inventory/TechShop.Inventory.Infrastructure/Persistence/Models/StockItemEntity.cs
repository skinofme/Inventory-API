using System;
using System.Collections.Generic;

namespace TechShop.Inventory.Infrastructure.Persistence.Models;

public partial class StockItemEntity
{
    public Guid IdStockItem { get; set; }

    public Guid IdWarehouse { get; set; }

    public string Sku { get; set; } = null!;

    public int QuantityAvailable { get; set; }

    public int QuantityReserved { get; set; }

    public int? QuantityTotal { get; set; }

    public DateTime LastUpdated { get; set; }

    public bool IsActive { get; set; }

    public virtual WarehouseEntity IdWarehouseNavigation { get; set; } = null!;

    public virtual ICollection<InventoryMovementEntity> InventoryMovements { get; set; } = new List<InventoryMovementEntity>();

    public virtual ICollection<StockReservationEntity> StockReservations { get; set; } = new List<StockReservationEntity>();
}
