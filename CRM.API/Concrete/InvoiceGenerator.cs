using CRM.Business.Concrete;


namespace CRM.API.Concrete
{
	public class InvoiceGenerator
	{
		private UserManager _userManager;
		private OrderManager _orderManager;
		private InvoiceManager _invoiceManager;

		public InvoiceGenerator(UserManager userManager, OrderManager orderManager, InvoiceManager invoiceManager)
		{
			_userManager = userManager;
			_orderManager = orderManager;
			_invoiceManager = invoiceManager;
		}

		//public void GenerateInvoices()
		//{
		//	List<User> userList = _userManager.GetListAll();

		//	foreach (User user in userList)
		//	{
		//		if (user.Orders.Count != 0)
		//		{
		//			continue;
		//		}
		//		float price = 0;
		//		foreach (Order order in user.Orders)
		//		{
		//			if (order.Product.ProductTypeId == 1 && order.Lifetimes!.Collected) // If product type is lifetime license
		//			{
		//				price += order.Product.Price;
		//			}

		//			new Invoice
		//			{
		//				ExcessAmount = user.Quota - price,
		//				InvoinceStartDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1)),
		//				InvoinceEndDate = DateOnly.FromDateTime(DateTime.Now),

		//			};
		//		}
		//	}
		//	Console.WriteLine("Invoice generated");
		//}
	}
}
