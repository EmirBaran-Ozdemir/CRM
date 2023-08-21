using CRM.DataAccess.Abstract;
using CRM.DataAccess.Concrete;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess.EntityFramework
{
	public class EFOrderRepo : GenericRepo<Order>, IOrderDal
	{
		CRMContext context = new CRMContext();

		public Order GetOrder(int customerId)
		{
			Order order = context.Orders.Include(x => x.CustomerId == customerId).FirstOrDefault()!;
			return order;
		}
		public List<Order> GetAllWithSellerInfo(int id)
		{
			var order = context.Orders.Include(x => x.Customer).Include(x => x.Product).Include(x=>x.Product.Seller).Include(x => x.Product.Seller!.Company).Include(x => x.Product.ProductType).ToList();
			return order;
		}
	}
}
