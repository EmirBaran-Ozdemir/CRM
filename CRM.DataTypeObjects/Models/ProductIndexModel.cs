using CRM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataTypeObjects.Models
{
	public class ProductIndexModel
	{
		public CreateOrderModel CreateOrderModel { get; set; } = null!;
		public List<Product> Products { get; set; } = null!;
	}
}
