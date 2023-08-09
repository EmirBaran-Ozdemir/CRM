using CRM.Business.Abstract;
using CRM.DataAccess.Abstract;
using CRM.Entity.Concrete;
using System.Linq.Expressions;

namespace CRM.Business.Concrete
{
	public class ProductManager : GenericManager<Product>, IProductSerivce
	{
		IProductDal _productDal;
		public ProductManager(IProductDal productDal)
		{
			_productDal = productDal;
		}
		public List<Product> GetListAll(Expression<Func<Product, bool>> filter)
		{
			return _productDal.GetListAll(filter);
		}
		public bool CheckSameLicense(string license)
		{
			return _productDal.CheckSameLicense(license);
		}
		public bool CheckSameProduct(string name)
		{
			return _productDal.CheckSameProduct(name);
		}
		public Product GetProductWithSellerAndCompanyAndProductTypeById(int id)
		{
			return _productDal.GetProductWithSellerAndCompanyAndProductTypeById(id);
		}
	}
}
