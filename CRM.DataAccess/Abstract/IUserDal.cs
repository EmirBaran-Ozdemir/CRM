using CRM.Entity.Concrete;

namespace CRM.DataAccess.Abstract
{
	public interface IUserDal : IGenericDal<User>
	{
		List<Company> GetCompanies();
		public User GetUserWithCompanyById(int id);
		public User GetUserWithCompanyAndProductsById(int id);
		public User? LoginUser(User model);
	}
}
