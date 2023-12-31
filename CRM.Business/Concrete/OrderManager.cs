﻿using CRM.Business.Abstract;
using CRM.DataAccess.Abstract;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;

namespace CRM.Business.Concrete
{
	public class OrderManager : GenericManager<Order>, IOrderService
	{
		IOrderDal _orderDal;
		public OrderManager(GenericRepo<Order> repo, IOrderDal orderDal)
			: base(repo)
		{
			_orderDal = orderDal;
		}
		public Order CreateOrder(Product product, int customerId)
		{
			Order order = new Order
			{
				CustomerId = customerId,
				ProductId = product.Id,
				OrderDate = DateOnly.FromDateTime(DateTime.Now.Date),
				CurrentPrice = product.Price,
			};

			return order;
		}

		public List<Order> GetAllWithSellerInfo(int id)
		{
			return _orderDal.GetAllWithSellerInfo(id);
		}
	}
}
