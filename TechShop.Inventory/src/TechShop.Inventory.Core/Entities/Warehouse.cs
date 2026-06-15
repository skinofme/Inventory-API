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
		

		private Warehouse() { }

		
		public static Warehouse Create(string code, string name, string location)
		{
			ValidateState(code, name, location);

			return new Warehouse()
			{
				IdWarehouse = Guid.NewGuid(),
				Code = code,
				Name = name,
				Location = location,
				IsActive = true
			};
		}

		internal static Warehouse Rehydrate(Guid idWarehouse, string code, string name, string location, bool isActive)
		{
			if (idWarehouse == Guid.Empty) throw new InvalidIdWarehouseException(idWarehouse);

			ValidateState(code, name, location);

			return new Warehouse()
			{
				IdWarehouse = idWarehouse,
				Code = code,
				Name = name,
				Location = location,
				IsActive = isActive
			};
		}

		public void Activate()
		{
			if (IsActive) return;
			IsActive = true;
		}

		public void Deactivate()
		{
			if (!IsActive) return;
			IsActive = false;
		}

		private static void ValidateState(string code, string name, string location)
		{
			if (string.IsNullOrWhiteSpace(code)) throw new InvalidWarehouseCodeException(code);
			if (string.IsNullOrWhiteSpace(name)) throw new InvalidNameException(name);
			if (string.IsNullOrWhiteSpace(location)) throw new InvalidLocationException(location);
		}
	}
}
