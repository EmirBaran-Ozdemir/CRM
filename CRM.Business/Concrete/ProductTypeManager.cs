using CRM.Business.Abstract;
using CRM.DataAccess.Abstract;
using CRM.Entity.Concrete;

namespace CRM.Business.Concrete
{
	public class ProductTypeManager : GenericManager<ProductType>, IProductTypeService
	{
		readonly IProductTypeDal _productTypeDal;
		public ProductTypeManager(IProductTypeDal productTypeDal)
		{
			_productTypeDal = productTypeDal;
		}
	}
}
