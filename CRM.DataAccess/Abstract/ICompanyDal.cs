using CRM.Entity.Concrete;

namespace CRM.DataAccess.Abstract
{
	public interface ICompanyDal : IGenericDal<Company>
	{
		List<User> GetUsersWithRolesByCompanyId(int id);
		List<User> GetCompanyEmployees(int id);
		bool CheckCompany(string name);
	}
}
