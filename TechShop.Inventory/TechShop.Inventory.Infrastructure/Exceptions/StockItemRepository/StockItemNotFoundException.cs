using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Inventory.Infrastructure.Exceptions.StockItemRepository
{
	public class StockItemNotFoundException : InfrastructureException
	{
		public StockItemNotFoundException(int idStockItem) : base($"The source requested: {idStockItem} cannot be found.")
		{ 
		}
	}
}
