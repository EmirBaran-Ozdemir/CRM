using CRM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataTypeObjects.Models
{
	public class ListInvoicesModel
	{
		public List<Invoice>? Invoices { get; set; }
		public List<Order>? Orders { get; set; }
	}
}
