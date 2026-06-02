using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Inventory.Core.Exceptions.common
{
	internal class InvalidIdException : DomainException
	{
		public InvalidIdException(Guid id)
			: base($"The requested Id: {id}, cannot be empty")
		{
		}
	}
}
