using CRM.Business.Abstract;
using CRM.DataAccess.Abstract;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;

namespace CRM.Business.Concrete
{
	internal class LifetimeManager : GenericManager<Lifetime>, ILifetimeService
	{
		readonly ILifetimeDal _lifetimeDal;
		public LifetimeManager(GenericRepo<Lifetime> repo, ILifetimeDal lifetimeDal)
			: base(repo)
		{
			_lifetimeDal = lifetimeDal;
		}
	}
}
