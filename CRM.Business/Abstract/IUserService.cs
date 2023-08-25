using CRM.Entity.Concrete;
using Microsoft.Extensions.Configuration;

namespace CRM.Business.Abstract
{
	public interface IUserService : IGenericService<User>
	{
		public List<Company> GetCompanies();
		public List<User> GetAllUsersWithOrders();
		public User GetUserWithCompanyById(int id);

		public User GetUserWithCompanyAndProductsById(int id);
		public User? LoginUser(User model);

	}
}
