using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace CRM.WebUI.Controllers
{
	[AllowAnonymous]
	public class ErrorController : Controller
	{
		private readonly ICompositeViewEngine _viewEngine;

		public ErrorController(ICompositeViewEngine viewEngine)
		{
			_viewEngine = viewEngine;
		}
		private bool ViewExists(string name)
		{
			ViewEngineResult viewEngineResult = _viewEngine.FindView(ControllerContext, name, true);
			return viewEngineResult?.View != null;
		}

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