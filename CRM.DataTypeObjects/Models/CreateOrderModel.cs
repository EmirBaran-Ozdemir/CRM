using CRM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataTypeObjects.Models
{
	public class CreateOrderModel
	{
		public Product Product { get; set; }
		public DateOnly? endDate { get; set; } = null;
	}
}
