using CRM.Entity.Concrete;

namespace CRM.DataTypeObjects.Models
{
	public class CompanyProductsModel
	{
		public Company? Company { get; set; }
		public List<Product>? Products { get; set; } = new List<Product>();
	}
}
