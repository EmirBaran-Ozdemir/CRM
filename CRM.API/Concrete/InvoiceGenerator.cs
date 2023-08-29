using CRM.Business.Abstract;
using CRM.Business.Concrete;
using CRM.Entity.Concrete;
using Serilog;
using System.Linq;


namespace CRM.API.Concrete
{
	public interface IInvoiceGenerationService
	{
		void GenerateInvoices();
		void SetMembershipPaymentStatus(Order order);
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
				Log.Information($"Invoice generating for {user.Name}");
				float payment = 0;
				foreach (Order order in user.Orders)
				{
					payment += GetPayment(order);
				}

				Invoice invoice = new Invoice
				{
					UserId = user.Id,
					ExcessAmount = user.Quota - payment,
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
		}

		public void SetMembershipPaymentStatus(Order order)
		{
			if (order.Membership != null)
			{
				if (order.Membership!.EndDate == DateOnly.FromDateTime(DateTime.Now))
				{
					order.Membership!.PaymentCollected = false;
					Log.Information("Set payment status to not paid");
				}
				else
					Log.Information($"{order.Product.Name}'s membership has finished");

			}
		}

		private float GetPayment(Order order)
		{
			float payment = 0;
			if (order.Lifetime != null && !order.Lifetime.PaymentCollected)
			{
				payment = order.CurrentPrice;
				order.Lifetime.PaymentCollected = true;
				Log.Information($" - - - Lifetime payment collected for order: {order}");
			}

			if (order.Membership != null && !order.Membership.PaymentCollected)
			{
				payment = order.CurrentPrice;
				order.Membership.PaymentCollected = true;
				Log.Information($" - - - Membership payment collected for order: {order}");
			}
			return payment;
		}
	}

}
