using CRM.Entity.Concrete;

namespace CRM.Business.Abstract
{
	public interface IInvoiceService : IGenericService<Invoice>
	{
		public List<Invoice> GetInvoicesWithOrdersAndProductsByUserId(int id);
	}
}
