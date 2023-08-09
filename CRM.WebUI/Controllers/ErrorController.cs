using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Serilog;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace CRM.WebUI.Controllers
{
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
		[Route("Error/{statusCode}")]
        [AllowAnonymous]
        public IActionResult Error(int statusCode)
        {
            if(!ViewExists(statusCode.ToString()))
			{
				Log.Error("No default error view found");
				ViewBag.StatusCode = statusCode;
				return View("Error");
			}

			return View(statusCode.ToString());
			
		}
    }
}