using CRM.Business.Abstract;
using CRM.DataAccess.Abstract;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;

namespace CRM.Business.Concrete
{
	public class UserRoleManager : GenericManager<UserRole>, IUserRoleService
	{
		readonly IUserRoleDal _userRoleDal;
		public UserRoleManager(GenericRepo<UserRole> repo, IUserRoleDal userRoleDal) : base(repo)
		{
			_userRoleDal = userRoleDal;
		}
	}
}
