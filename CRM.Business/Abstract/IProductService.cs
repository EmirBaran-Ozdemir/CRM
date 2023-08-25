using CRM.DataAccess.Abstract;
using CRM.Entity.Concrete;
using System.Linq.Expressions;

namespace CRM.Business.Abstract
{
	public interface IProductService : IGenericService<Product>
	{
		public List<Product> GetListAll(Expression<Func<Product, bool>> filter);
		public List<Product> GetAllProductsWithTypesBySellerId(int id);
		public List<Product> GetAllWithCompanyAndProductType();
		public bool CheckSameLicense(string license);
		public bool CheckSameProduct(string name);
		public Product GetProductWithSellerInfoAndProductTypeById(int id);
	}
}
