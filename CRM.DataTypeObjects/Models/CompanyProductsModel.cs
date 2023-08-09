using CRM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataTypeObjects.Models
{
	public class CompanyProductsModel
	{
		public Company? Company { get; set; }
		public List<Product>? Products { get; set; } = new List<Product>();
	}
}
