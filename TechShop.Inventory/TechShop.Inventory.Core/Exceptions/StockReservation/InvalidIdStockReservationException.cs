using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Inventory.Core.Exceptions.StockReservation
{
	public class InvalidIdStockReservationException : DomainException
	{
		public InvalidIdStockReservationException(Guid idStockReservation)
			: base($"Invalid requested IdstockReservation: {idStockReservation}, cannot be empty")
		{ }
	}
}
