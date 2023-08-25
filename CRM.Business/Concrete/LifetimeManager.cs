using CRM.Business.Abstract;
using CRM.DataAccess.Abstract;
using CRM.Entity.Concrete;

namespace CRM.Business.Concrete
{
	internal class LifetimeManager : GenericManager<Lifetime>, ILifetimeService
	{
		readonly ILifetimeDal _lifetimeDal;
		public LifetimeManager(ILifetimeDal lifetimeDal)
		{
			_lifetimeDal = lifetimeDal;
		}
	}
}
