using CRM.Business.Abstract;
using CRM.DataAccess.Abstract;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;

namespace CRM.Business.Concrete
{
	public class MembershipManager : GenericManager<Membership>, IMembershipService
	{
		readonly IMembershipDal _membershipDal;
		public MembershipManager(GenericRepo<Membership> repo, IMembershipDal membershipDal)
			: base(repo)
		{
			_membershipDal = membershipDal;
		}
	}
}
