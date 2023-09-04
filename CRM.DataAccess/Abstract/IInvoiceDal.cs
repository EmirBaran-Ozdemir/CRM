using CRM.Entity.Concrete;

namespace CRM.DataAccess.Abstract
{
	public interface IInvoiceDal : IGenericDal<Invoice>
	{
		public List<Invoice> GetInvoicesWithOrdersAndProductsByUserId(int id);

	}
}
