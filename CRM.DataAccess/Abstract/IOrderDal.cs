using CRM.Entity.Concrete;

namespace CRM.DataAccess.Abstract
{
	public interface IOrderDal : IGenericDal<Order>
	{
		public Order GetOrder(int customerId);
		public List<Order> GetAllWithSellerInfo(int id);
	}
}
