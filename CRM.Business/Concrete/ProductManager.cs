using CRM.Business.Abstract;
using CRM.DataAccess.Abstract;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;
using System.Linq.Expressions;

namespace CRM.Business.Concrete
{
	public class ProductManager : GenericManager<Product>, IProductService
	{
		IProductDal _productDal;
		public ProductManager(GenericRepo<Product> repo, IProductDal productDal) 
			: base(repo)
		{
			_productDal = productDal;
		}
		public List<Product> GetListAll(Expression<Func<Product, bool>> filter)
		{
			return _productDal.GetListAll(filter);
		}
		public List<Product> GetAllProductsWithTypesBySellerId(int id)
		{
			return _productDal.GetAllProductsWithTypesBySellerId(id);
		}
		public List<Product> GetAllWithCompanyAndProductType()
		{
			return _productDal.GetAllWithCompanyAndProductType();
		}
		public bool CheckSameLicense(string license)
		{
			return _productDal.CheckSameLicense(license);
		}
		public bool CheckSameProduct(string name)
		{
			return _productDal.CheckSameProduct(name);
		}
		public Product GetProductWithSellerInfoAndProductTypeById(int id)
		{
			return _productDal.GetProductWithSellerInfoAndProductTypeById(id);
		}
	}
}
