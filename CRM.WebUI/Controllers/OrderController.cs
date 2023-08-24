using CRM.Business.Concrete;
using CRM.DataAccess.EntityFramework;
using CRM.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebUI.Controllers
{
	public class OrderController : Controller
	{
		readonly OrderManager _manager = new OrderManager(new EFOrderRepo());

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
		public IActionResult CreateOrder(Product product)
		{
			int id = int.Parse(HttpContext.User.Identity!.Name!);
			var order = _manager.CreateOrder(product, id);
			_manager.Add(order);

			if (product.ProductTypeId == 1)
			{
				order.Lifetime = new Lifetime
				{
					OrderId = order.Id,
					PaymentCollected = false
				};
			}
			else
			{
				order.Membership = new Membership
				{
					OrderId = order.Id,
					StartDate = DateOnly.FromDateTime(DateTime.Now.Date),
					EndDate = DateOnly.FromDateTime(DateTime.Now.Date.AddYears(1)),
				};
			}
			_manager.Update(order);

			TempData["ProcessStatus"] = true;
			TempData["ProcessMessage"] = "Order added succesfully.";
			var model = _manager.GetAllWithSellerInfo(id);
			return RedirectToAction("Index", model);
		}
	}
}
