using CRM.DataAccess.Abstract;
using CRM.DataAccess.Concrete;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;

namespace CRM.DataAccess.EntityFramework
{
	public class EFProductTypeRepo : GenericRepo<ProductType>, IProductTypeDal
	{
		public EFProductTypeRepo(CRMContext context) : base(context)
		{
		}
	}
}
