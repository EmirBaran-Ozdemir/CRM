using CRM.Business.Abstract;
using CRM.DataAccess.Abstract;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;

namespace CRM.Business.Concrete
{
	public class ProductTypeManager : GenericManager<ProductType>, IProductTypeService
	{
		readonly IProductTypeDal _productTypeDal;
		public ProductTypeManager(GenericRepo<ProductType> repo, IProductTypeDal productTypeDal)
			: base(repo)
		{
			_productTypeDal = productTypeDal;
		}
	}
}
