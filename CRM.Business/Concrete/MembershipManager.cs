using CRM.Business.Abstract;
using CRM.DataAccess.Abstract;
using CRM.Entity.Concrete;

namespace CRM.Business.Concrete
{
	public class MembershipManager : GenericManager<Membership>, IMembershipService
	{
		readonly IMembershipDal _membershipDal;
		public MembershipManager(IMembershipDal membershipDal)
		{
			_membershipDal = membershipDal;
		}
	}
}
