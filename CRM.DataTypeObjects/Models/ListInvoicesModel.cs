using CRM.Entity.Concrete;

namespace CRM.DataTypeObjects.Models
{
	public class ListInvoicesModel
	{
		public List<Invoice>? Invoices { get; set; }
		public List<Order>? Orders { get; set; }
	}
}
