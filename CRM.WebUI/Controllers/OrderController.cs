using CRM.Business.Abstract;
using CRM.Business.Concrete;
using CRM.DataAccess.EntityFramework;
using CRM.DataTypeObjects.Models;
using CRM.Entity.Concrete;
using Hangfire.Server;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebUI.Controllers
{
	public class OrderController : Controller
	{
		readonly IOrderService _manager;
		public OrderController(IOrderService manager)
		{
			_manager = manager;
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
		public IActionResult CreateOrder(CreateOrderModel model)
		{
			int id = int.Parse(HttpContext.User.Identity!.Name!);

			var order = _manager.CreateOrder(model.Product, id);
			_manager.Add(order);

			if (model.Product.ProductTypeId == 1)
			{
				GenerateLifetimeOrder(order);
			}
			else
			{
				GenerateMembershipOrder(order, DateOnly.FromDateTime(DateTime.Now.Date), model.endDate!.Value);
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
		}
	}
}
