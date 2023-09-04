using CRM.DataAccess.Abstract;
using CRM.DataAccess.Concrete;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess.EntityFramework
{
	public class EFCompanyRepo : GenericRepo<Company>, ICompanyDal
	{
		private readonly CRMContext _context;
		public EFCompanyRepo(CRMContext context) : base(context)
		{
			_context = context;
		}

		public List<User> GetUsersWithRolesByCompanyId(int id)
		{
			var values = _context.Users.Include(x => x.Role).Where(x => x.CompanyId == id).ToList();
			return values;
		}
		public List<User> GetCompanyEmployees(int id)
		{
			return _context.Users.Include(x => x.Role).Where(x => x.CompanyId == id).ToList();
		}
		public bool CheckCompany(string name)
		{
			return _context.Companies.FirstOrDefault(x => x.Name == name) != null;
		}
	}
}
