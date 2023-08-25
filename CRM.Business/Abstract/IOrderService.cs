using CRM.DataAccess.Abstract;
using CRM.Entity.Concrete;

namespace CRM.Business.Abstract
{
	public interface IOrderService : IGenericService<Order>
	{
		public Order CreateOrder(Product product, int customerId);

		public List<Order> GetAllWithSellerInfo(int id);
	}
}
