using CRM.DataAccess.EntityFramework;
using CRM.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using CRM.Business.Concrete;
using CRM.Business.ValidationRules;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;

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
			TempData["ProcessStatus"] = true;
			TempData["ProcessMessage"] = "Order added succesfully.";
			var model = _manager.GetAllWithSellerInfo(id);
			return RedirectToAction("Index", model);
		}
	}
}
