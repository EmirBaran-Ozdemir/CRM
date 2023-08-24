using CRM.Business.Abstract;
using CRM.DataAccess.Abstract;
using CRM.Entity.Concrete;

namespace CRM.Business.Concrete
{
	public class InvoiceManager : GenericManager<Invoice>, IInvoiceService
	{
		readonly IInvoiceDal _invoiceDal;
		public InvoiceManager(IInvoiceDal invoiceDal)
		{
			_invoiceDal = invoiceDal;
		}
	}
}
