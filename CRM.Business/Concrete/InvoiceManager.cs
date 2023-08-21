using CRM.Business.Abstract;
using CRM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Business.Concrete
{
	public class InvoiceManager : GenericManager<Invoice>, IInvoiceService
	{
	}
}
