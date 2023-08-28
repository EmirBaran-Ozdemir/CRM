using CRM.Business.Abstract;
using CRM.Business.Concrete;
using CRM.Entity.Concrete;
using System.Linq;


namespace CRM.API.Concrete
{
	public interface IInvoiceGenerationService
	{
		void GenerateInvoices();
	}

	public class InvoiceGenerationService : IInvoiceGenerationService
	{
		private IUserService _userManager;
		private IInvoiceService _invoiceManager;

		public InvoiceGenerationService(IUserService userManager, IInvoiceService invoiceManager)
		{
			_userManager = userManager;
			_invoiceManager = invoiceManager;
		}

		public void GenerateInvoices()
		{

			var usersWithOrders = _userManager.GetAllUsersWithOrdersAndTypes();

			foreach (User user in usersWithOrders)
			{
				float price = 0;
				foreach(Order order in user.Orders)
				{
					if (order.Lifetime == null)
						continue;
					if(order.Lifetime.PaymentCollected == true)
						continue;
					price += order.CurrentPrice;
				}
				Invoice invoice = new Invoice
				{
					UserId = user.Id,
					ExcessAmount = user.Quota - price,
					InvoinceStartDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1)),
					InvoinceEndDate = DateOnly.FromDateTime(DateTime.Now)
				};
				invoice = _invoiceManager.AddAndGet(invoice);

				foreach (Order order in user.Orders)
				{
					order.InvoiceId = invoice.Id;
				}
				_invoiceManager.Update(invoice);

			}
			Console.WriteLine("Invoice generated");
		}
	}
}
