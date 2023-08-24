using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebUI.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{

		[AllowAnonymous]
		public IActionResult Index()
		{
			ViewBag.ProcessStatus = TempData.ContainsKey("ProcessStatus") && (bool)TempData["ProcessStatus"]!;
			ViewBag.ProcessMessage = TempData["ProcessMessage"] as string;
			return View();
		}

	}
}