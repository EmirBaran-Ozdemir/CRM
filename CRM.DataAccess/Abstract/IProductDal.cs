using CRM.Entity.Concrete;

namespace CRM.DataAccess.Abstract
{
	public interface IProductDal : IGenericDal<Product>
	{
		List<Product> GetAllProductsWithTypesByTypeId(int id);
		Product GetProductWithSellerInfoAndProductTypeById(int id);

		List<Product> GetAllProductsWithTypesBySellerId(int id);
		bool CheckSameProduct(string name);
		bool CheckSameLicense(string license);

	}
}
