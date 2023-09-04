using CRM.Entity.Concrete;

namespace CRM.Business.Abstract
{
	public interface IUserService : IGenericService<User>
	{
		public List<Company> GetCompanies();
		public List<User> GetAllUsersWithOrders();
		public List<User> GetAllUsersWithOrdersAndTypes();
		public User GetUserWithCompanyById(int id);

		public User GetUserWithCompanyAndProductsById(int id);
		public User? LoginUser(User model);

	}
}
