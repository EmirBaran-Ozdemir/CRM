using CRM.DataAccess.Abstract;
using CRM.DataAccess.Concrete;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess.EntityFramework
{
	public class EFInvoiceRepo : GenericRepo<Invoice>, IInvoiceDal
	{
		private readonly CRMContext _context;
		public EFInvoiceRepo(CRMContext context) : base(context)
		{
			_context = context;
		}
		public List<Invoice> GetInvoicesWithOrdersAndProductsByUserId(int id)
		{
			List<Invoice> invoices = _context.Invoices
				.Include(i => i.Orders)
				.ThenInclude(o => o.Product)
				.Where(x => x.UserId == id)
				.ToList();

			return invoices;
		}
	}
}
