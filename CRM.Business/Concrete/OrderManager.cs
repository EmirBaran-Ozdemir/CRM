using CRM.Business.Abstract;
using CRM.DataAccess.Abstract;
using CRM.Entity.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Business.Concrete
{
	public class OrderManager : GenericManager<Order>, IOrderService
	{
		IOrderDal _orderDal;
		public OrderManager(IOrderDal orderDal)
		{
			_orderDal = orderDal;
		}
		public Order CreateOrder(Product product, int customerId)
		{
			Order order = new Order();
			order.CustomerId = customerId;
			order.ProductId = product.Id;
			order.OrderDate = DateOnly.FromDateTime(DateTime.Now.Date);
			order.CurrentPrice = product.Price;
			return order;
		}
		public Order GetOrder(int customerId)
		{
			return _orderDal.GetOrder(customerId);
		}
	}
}
