using TechShop.Inventory.Core.Exceptions.common;
using TechShop.Inventory.Core.Exceptions.Warehouse;

namespace TechShop.Inventory.Core.Entities
{
	public class Warehouse
	{
		public Guid IdWarehouse { get; private set; }

		public string Code { get; private set; }

		public string Name { get; private set; }

		public string Location { get; private set; }
		
		public bool IsActive { get; private set; }
		
		protected Warehouse() { }

		// constructor to create a new entity
		public Warehouse(string code, string name, string location)
		{
			if(string.IsNullOrWhiteSpace(code)) throw new InvalidWarehouseCodeException(code);
			if(string.IsNullOrWhiteSpace(name)) throw new InvalidNameException(name);
			if(string.IsNullOrWhiteSpace(location)) throw new InvalidLocationException(location);
			IdWarehouse = Guid.NewGuid();
			Code = code;
			Name = name;
			Location = location;
			IsActive = true;
		}

		// constructor to rehydrate the entity
		internal Warehouse(Guid idWarehouse, string code, string name, string location, bool isActive)
			:this(code, name, location)
		{
			if (idWarehouse == Guid.Empty) throw new InvalidIdException(idWarehouse);

			IdWarehouse = idWarehouse;
			IsActive = isActive;
		}
	}
}
