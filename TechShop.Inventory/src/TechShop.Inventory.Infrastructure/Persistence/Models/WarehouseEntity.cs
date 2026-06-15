using System;
using System.Collections.Generic;

namespace TechShop.Inventory.Infrastructure.Persistence.Models;

public partial class WarehouseEntity
{
    public Guid IdWarehouse { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<StockItemEntity> StockItems { get; set; } = new List<StockItemEntity>();
}
