using CRM.Entity.Concrete;

namespace CRM.Business.Abstract
{
	internal interface ICompanyService : IGenericService<Company>
	{
		public List<User> GetCompanyEmployees(int id);
		List<User> GetUsersWithRolesByCompanyId(int id);
	}
}
