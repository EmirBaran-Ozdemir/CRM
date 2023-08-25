using CRM.Business.Abstract;
using CRM.DataAccess.Abstract;
using CRM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Business.Concrete
{
	public class OrderInvoiceMapManager : GenericManager<OrderInvoiceMap>, IOrderInvoiceMapService
	{
		readonly IOrderInvoiceMapDal _orderInvoiceMapDal;
		public OrderInvoiceMapManager(IOrderInvoiceMapDal orderInvoiceMapDal)
		{
			_orderInvoiceMapDal = orderInvoiceMapDal;
		}
	}
}
