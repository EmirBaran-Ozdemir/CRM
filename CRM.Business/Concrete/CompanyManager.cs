using CRM.Business.Abstract;
using CRM.DataAccess.Abstract;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;

namespace CRM.Business.Concrete
{
	public class CompanyManager : GenericManager<Company>, ICompanyService
	{
		ICompanyDal _companyDal;

		public CompanyManager(GenericRepo<Company> repo, ICompanyDal companyDal)
			: base(repo)
		{
			_companyDal = companyDal;
		}
		public List<User> GetCompanyEmployees(int id)
		{
			return _companyDal.GetCompanyEmployees(id);
		}
		public List<User> GetUsersWithRolesByCompanyId(int id)
		{
			return _companyDal.GetUsersWithRolesByCompanyId(id);
		}
		public bool CheckCompany(string name)
		{
			return _companyDal.CheckCompany(name);
		}
	}
}
