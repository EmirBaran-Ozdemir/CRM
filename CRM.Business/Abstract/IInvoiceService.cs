using CRM.Entity.Concrete;
using Microsoft.AspNetCore.Routing.Matching;

namespace CRM.Business.Abstract
{
	public interface IInvoiceService : IGenericService<Invoice>
	{
		public List<Invoice> GetInvoicesWithOrdersAndProductsByUserId(int id);
	}
}
