using CRM.Business.Abstract;
using CRM.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebUI.Controllers
{
	public class InvoiceController : Controller
	{
		readonly IUserService _userManager;
		readonly IInvoiceService _invoiceService;

		public InvoiceController(IUserService userManager, IInvoiceService invoiceService)
		{
			_userManager = userManager;
			_invoiceService = invoiceService;
		}

		public IActionResult MyInvoices()
		{

			string identityId = HttpContext.User.Identity!.Name!;

			if (int.TryParse(identityId, out int id))
			{
				User user = _userManager.GetUserWithCompanyAndProductsById(id);
				List<Invoice> invoices = _invoiceService.GetInvoicesWithOrdersAndProductsByUserId(user.Id);

				if (user != null)
				{
					return View(invoices);
				}
			}

			TempData["ProcessStatus"] = false;
			TempData["ProcessMessage"] = "You must be logged in.";
			return RedirectToAction("Login", "Auth");
		}
	}
}
