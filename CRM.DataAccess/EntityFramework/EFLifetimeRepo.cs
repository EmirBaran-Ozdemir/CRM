using CRM.DataAccess.Abstract;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;

namespace CRM.DataAccess.EntityFramework
{
	internal class EFLifetimeRepo : GenericRepo<Lifetime>, ILifetimeDal
	{
	}
}
