using CRM.Business.Abstract;
using CRM.DataAccess.Abstract;
using CRM.Entity.Concrete;

namespace CRM.Business.Concrete
{
	public class CompanyManager : GenericManager<Company>, ICompanyService
	{
		ICompanyDal _companyDal;

		public CompanyManager(ICompanyDal companyDal)
		{
			_companyDal = companyDal;
		}

		public List<User> GetUsersWithProductsByCompanyId(int id)
		{
			return _companyDal.GetUsersWithProductsByCompanyId(id);
		}
		public List<User> GetUsersWithRolesByCompanyId(int id)
		{
			return _companyDal.GetUsersWithRolesByCompanyId(id);
		}
	}
}
