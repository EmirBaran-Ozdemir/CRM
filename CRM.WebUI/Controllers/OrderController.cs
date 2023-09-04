using CRM.API.Concrete;
using CRM.Business.Abstract;
using CRM.Business.Concrete;
using CRM.DataAccess.EntityFramework;
using CRM.DataTypeObjects.Models;
using CRM.Entity.Concrete;
using CRM.WebUI.PackageConf;
using Hangfire.Server;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebUI.Controllers
{
	public class OrderController : Controller
	{
		readonly IOrderService _manager;
		readonly HangfireJobScheduler _hangfireJobScheduler;
		readonly IInvoiceGenerationService _invoiceGenerationService;
		public OrderController(IOrderService manager, HangfireJobScheduler hangfireJobScheduler, IInvoiceGenerationService invoiceGenerationService)
		{
			_manager = manager;
			_hangfireJobScheduler = hangfireJobScheduler;
			_invoiceGenerationService = invoiceGenerationService;
		}

		[Authorize(Policy = "Buyer")]
		public IActionResult Index()
		{
			int id = int.Parse(HttpContext.User.Identity!.Name!);
			var model = _manager.GetAllWithSellerInfo(id);
			ViewBag.ProcessStatus = TempData.ContainsKey("ProcessStatus") && (bool)TempData["ProcessStatus"]!;
			ViewBag.ProcessMessage = TempData["ProcessMessage"] as string;
			return View(model);
		}

		[HttpPost("buy")]
		[Authorize(Policy = "Buyer")]
		public IActionResult CreateOrder(ProductIndexModel model)
		{
			int id = int.Parse(HttpContext.User.Identity!.Name!);

			var order = _manager.CreateOrder(model.CreateOrderModel.Product!, id);
			_manager.Add(order);
			order.Product = model.CreateOrderModel.Product!;
			if (model.CreateOrderModel.Product!.ProductTypeId == 1)
			{
				GenerateLifetimeOrder(order);
			}
			else
			{
				DateOnly initialEndDate = DateOnly.Parse(model.CreateOrderModel.endDate!);
				DateOnly modifiedEndDate = new DateOnly(initialEndDate.Year, initialEndDate.Month, DateTime.Now.Day);
				GenerateMembershipOrder(order, DateOnly.FromDateTime(DateTime.Now.Date), modifiedEndDate);
			}

			_manager.Update(order);

			TempData["ProcessStatus"] = true;
			TempData["ProcessMessage"] = "Order added succesfully.";
			var orderList = _manager.GetAllWithSellerInfo(id);
			return RedirectToAction("Index", orderList);
		}

		private void GenerateLifetimeOrder(Order order)
		{
			order.Lifetime = new Lifetime
			{
				OrderId = order.Id,
				PaymentCollected = false
			};

		}

		private void GenerateMembershipOrder(Order order, DateOnly startDate, DateOnly endDate)
		{
			order.Membership = new Membership
			{
				OrderId = order.Id,
				StartDate = startDate,
				EndDate = endDate,
			};
			_hangfireJobScheduler.AddJob(order);

		}
	}
}
