using CRM.DataAccess.EntityFramework;
using CRM.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using CRM.Business.Concrete;
using CRM.Business.ValidationRules;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CRM.WebUI.Controllers
{
	public class OrderController : Controller
	{
		OrderManager _manager = new OrderManager(new EFOrderRepo());
		public OrderController(OrderManager manager)
		{
			_manager = manager;
		}

		[HttpPost("buy")]
		public IActionResult CreateOrder(Product product)
		{
			var order = _manager.CreateOrder(product, int.Parse(HttpContext.User.Identity!.Name!));
			_manager.Add(order);
			AddProductVal validationRules = new AddProductVal();
			var result = validationRules.Validate(product);
			if(!result.IsValid)
			{
				ViewBag.Errors = result.Errors;
				return View("");
			}
			return View("Auth", "UserOrders");
		}
	}
}
