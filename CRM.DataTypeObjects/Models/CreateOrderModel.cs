using CRM.Entity.Concrete;

namespace CRM.DataTypeObjects.Models
{
	public class CreateOrderModel
	{
		public Product? Product { get; set; }
		public String? endDate { get; set; } = null;
	}
}
