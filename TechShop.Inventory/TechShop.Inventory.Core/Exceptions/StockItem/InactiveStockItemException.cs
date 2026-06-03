using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Inventory.Core.Exceptions.StockItem
{
	public class InactiveStockItemException : DomainException
	{
		public InactiveStockItemException(Guid idStockItem)
			:base($"The inactive Stock Item: {idStockItem} cannot be sold or reserved")
		{
		}
	}
}
