using CRM.Entity.Concrete;

namespace CRM.DataAccess.Abstract
{
	public interface IProductDal : IGenericDal<Product>
	{
		List<Product> GetProductsByProductTypeId(int id);
		Product GetProductWithSellerAndCompanyAndProductTypeById(int id);

		bool CheckSameProduct(string name);
		bool CheckSameLicense(string license);

	}
}
