using CRM.Business.Abstract;
using CRM.DataAccess.Concrete;
using CRM.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Serilog;


namespace CRM.API.Concrete
{
	public interface IInvoiceGenerationService
	{
		void GenerateInvoices();
		void SetMembershipPaymentStatus(Order order);
	}

	public class InvoiceGenerationService : IInvoiceGenerationService
	{
		readonly private IUserService _userManager;
		readonly private IInvoiceService _invoiceManager;
		readonly private IOrderService _orderManager;

		public InvoiceGenerationService(IUserService userManager, IInvoiceService invoiceManager, IOrderService orderManager)
		{
			_userManager = userManager;
			_invoiceManager = invoiceManager;
			_orderManager = orderManager;
		}

		public void GenerateInvoices()
		{
			var usersWithOrders = _userManager.GetAllUsersWithOrdersAndTypes();

			foreach (User user in usersWithOrders)
			{
				if (user.RoleId == 2)
				{

					Log.Information($"Invoice generating for {user.Name}");
					float payment = 0;
					foreach (Order order in user.Orders)
					{
						float lifetimePayment = GetPayment(order, o => o.Lifetime);
						float membershipPayment = GetPayment(order, o => o.Membership);
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
		}

		public void SetMembershipPaymentStatus(Order order)
		{
			if (order.Membership!.EndDate == DateOnly.FromDateTime(DateTime.Now))
			{
				order.Membership!.PaymentCollected = false;
				Log.Information("Set payment status to not paid");
			}
			else
				Log.Information($"{order.Product.Name}'s membership has finished");
		}

		private float GetPayment<T>(Order order, Func<Order, T?> propertyAccessor) where T : class
		{
			float payment = 0;
			T paymentInfo = propertyAccessor(order)!;

			if (paymentInfo != null && !(bool)paymentInfo.GetType().GetProperty("PaymentCollected")!.GetValue(paymentInfo)!)
			{
				payment = order.CurrentPrice;

				paymentInfo.GetType().GetProperty("PaymentCollected")!.SetValue(paymentInfo, true);
				_orderManager.Update(order);

				Log.Information($" - - - {paymentInfo.GetType()} payment collected for order: {order.Product.Name}");
			}
			return payment;
		}
	}
}
