using CRM.DataAccess.Abstract;
using CRM.DataAccess.Concrete;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;

namespace CRM.DataAccess.EntityFramework
{
	internal class EFLifetimeRepo : GenericRepo<Lifetime>, ILifetimeDal
	{
		public EFLifetimeRepo(CRMContext context) : base(context)
		{
		}
	}
}
