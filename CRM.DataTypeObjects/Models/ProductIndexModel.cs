using CRM.Entity.Concrete;

namespace CRM.DataTypeObjects.Models
{
	public class ProductIndexModel
	{
		public CreateOrderModel CreateOrderModel { get; set; } = null!;
		public List<Product> Products { get; set; } = null!;
	}
}
