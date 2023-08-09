using CRM.Entity.Concrete;

namespace CRM.Business.Abstract
{
	internal interface ICompanyService : IGenericService<Company>
	{
		List<User> GetUsersWithProductsByCompanyId(int id);
		List<User> GetUsersWithRolesByCompanyId(int id);
	}
}
