using CRM.DataAccess.Abstract;
using CRM.DataAccess.Concrete;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess.EntityFramework
{
	public class EFCompanyRepo : GenericRepo<Company>, ICompanyDal
	{
		CRMContext context = new CRMContext();

		public List<User> GetUsersWithRolesByCompanyId(int id)
		{
			var values = context.Users.Include(x => x.Role).Where(x => x.CompanyId == id).ToList();
			values = context.Users.Include(x => x.Company).ToList();
			return values;

		}

		public List<User> GetUsersWithProductsByCompanyId(int id)
		{
			var values = context.Users.Include(x => x.Products).Where(x => x.CompanyId == id).ToList();
			return values;
		}
	}
}
