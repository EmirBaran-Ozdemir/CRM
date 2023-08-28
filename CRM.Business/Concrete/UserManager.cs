using CRM.Business.Abstract;
using CRM.DataAccess.Abstract;
using CRM.Entity.Concrete;

namespace CRM.Business.Concrete
{
	public class UserManager : GenericManager<User>, IUserService
	{
		readonly IUserDal _userDal;
		public UserManager(IUserDal userDal)
		{
			_userDal = userDal;
		}

		public List<User> GetAllUsersWithOrders()
		{
			return _userDal.GetAllUsersWithOrders();
		}
		public List<Company> GetCompanies()
		{
			return _userDal.GetCompanies();
		}

		public User GetUserWithCompanyById(int id)
		{
			return _userDal.GetUserWithCompanyById(id);
		}

		public User GetUserWithCompanyAndProductsById(int id)
		{
			return _userDal.GetUserWithCompanyAndProductsById(id);
		}

		public User? LoginUser(User model)
		{
			return _userDal.LoginUser(model);
		}

		public List<User> GetAllUsersWithOrdersAndTypes()
		{
			return _userDal.GetAllUsersWithOrdersAndTypes();
		}
	}
}
