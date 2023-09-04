using CRM.Business.Abstract;
using CRM.DataAccess.Abstract;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;

namespace CRM.Business.Concrete
{
	public class InvoiceManager : GenericManager<Invoice>, IInvoiceService
	{
		readonly IInvoiceDal _invoiceDal;
		public InvoiceManager(GenericRepo<Invoice> repo, IInvoiceDal invoiceDal)
			: base(repo)
		{
			_invoiceDal = invoiceDal;
		}

		public List<Invoice> GetInvoicesWithOrdersAndProductsByUserId(int id)
		{
			return _invoiceDal.GetInvoicesWithOrdersAndProductsByUserId(id);
		}
	}
}
