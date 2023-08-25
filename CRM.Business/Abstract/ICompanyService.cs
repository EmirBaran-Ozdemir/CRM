using CRM.DataAccess.Abstract;
using CRM.Entity.Concrete;

namespace CRM.Business.Abstract
{
	public interface ICompanyService : IGenericService<Company>
	{
		public List<User> GetCompanyEmployees(int id);
		List<User> GetUsersWithRolesByCompanyId(int id);
		public bool CheckCompany(string name);
	}
}
