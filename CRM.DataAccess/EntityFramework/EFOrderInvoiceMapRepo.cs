using CRM.DataAccess.Abstract;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataAccess.EntityFramework
{
	public class EFOrderInvoiceMapRepo : GenericRepo<OrderInvoiceMap>, IOrderInvoiceMapDal
	{
	}
}
