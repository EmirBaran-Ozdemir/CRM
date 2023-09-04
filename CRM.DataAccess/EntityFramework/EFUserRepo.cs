using CRM.DataAccess.Abstract;
using CRM.DataAccess.Concrete;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;
using Microsoft.EntityFrameworkCore;


namespace CRM.DataAccess.EntityFramework
{
	public class EFUserRepo : GenericRepo<User>, IUserDal
	{
		private readonly CRMContext _context;
		public EFUserRepo(CRMContext context) : base(context)
		{
			_context = context;
		}

		public List<User> GetAllUsersWithOrders()
		{
			var values = _context.Users.Include(x => x.Orders).ToList();
			return values;
		}

		public List<User> GetAllUsersWithOrdersAndTypes()
		{
			var values = _context.Users
				.Include(u => u.Orders)
					.ThenInclude(o => o.Lifetime)
				.Include(u => u.Orders)
					.ThenInclude(o => o.Membership)
				.Include(u => u.Orders)
					.ThenInclude(o => o.Product)
				.ToList();
			return values;
		}
		public User GetUserWithCompanyById(int id)
		{
			var values = _context.Users.Include(x => x.Company).FirstOrDefault(x => x.Id == id);
			return values!;
		}
		public User GetUserWithCompanyAndProductsById(int id)
		{
			var values = _context.Users.Include(x => x.Company).Include(x => x.Products).Include(x => x.Role).FirstOrDefault(x => x.Id == id);
			return values!;
		}
		public User? LoginUser(User model) => _context.Users.Include(x => x.Role).FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);

		public List<Company> GetCompanies()
		{
			return _context.Companies.ToList();
		}

	}
}
