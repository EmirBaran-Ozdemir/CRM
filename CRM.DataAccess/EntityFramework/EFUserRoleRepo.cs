using CRM.DataAccess.Abstract;
using CRM.DataAccess.Concrete;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;

namespace CRM.DataAccess.EntityFramework
{
	public class EFUserRoleRepo : GenericRepo<UserRole>, IUserRoleDal
	{
		public EFUserRoleRepo(CRMContext context) : base(context)
		{
		}
	}
}
