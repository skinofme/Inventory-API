using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Inventory.Core.Enums.InventoryMovement;

namespace TechShop.Inventory.Core.Entities
{
	public class InventoryMovement
	{
		public int IdInventoryMovement { get; private set; }

		public int IdStockItem {  get; private set; }

		public MovementType MovementType { get; private set; }
		public int Quantity { get; private set; }
		public string? Reason {  get; private set; }

		public string? ReferenceId { get; private set; }



		// Constructor to create a new entity
		public InventoryMovement(int idStockItem, MovementType movementType, int quantity, string? reason, string? referenceId)
		{
			IdStockItem = idStockItem;
			MovementType = movementType;
			Quantity = quantity;
			Reason = reason;
			ReferenceId = referenceId;
		}

		// Constructor to rehydrate a entity
		internal InventoryMovement(int idInventoryMovement, int idStockItem, MovementType movementType, int quantity, string? reason, string? referenceId)
		{
			IdInventoryMovement = idInventoryMovement;
			IdStockItem = idStockItem;
			MovementType = movementType;
			Quantity = quantity;
			Reason = reason;
			ReferenceId = referenceId;
		}
	}
}
