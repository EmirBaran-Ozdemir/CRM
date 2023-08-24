using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebUI.Controllers
{
	[AllowAnonymous]
	public class ErrorController : Controller
	{
		[Route("Error")]
		public IActionResult Error(string message, int code)
		{
			if (message != null)
			{
				ViewData["ErrorMessage"] = message;
			}
			if (code != 0)
			{
				ViewData["ErrorCode"] = code;
			}
			return View();
		}
	}
}