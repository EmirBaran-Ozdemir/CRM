//using System;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.ViewEngines;

//namespace CRM.WebUI.Middleware
//{
//	public class ViewExistenceMiddleware : IMiddleware
//	{
//		private readonly ICompositeViewEngine _viewEngine;

//		public ViewExistenceMiddleware(ICompositeViewEngine viewEngine)
//		{
//			_viewEngine = viewEngine;
//		}

//		private bool ViewExists(string name)
//		{

//			ControllerContext controllerContext = new ControllerContext();

//			ViewEngineResult viewEngineResult = _viewEngine.FindView(controllerContext, name, true);
//			return viewEngineResult.View != null;
//		}

//		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
//		{
//			string path = context.Request.Path.Value!.TrimStart('/');

//			if (ViewExists(path))
//			{
//				await next.Invoke(context);
//			}
//			else
//			{
//				throw new NotFoundException("Page not found");
//			}
//		}
//	}
//}
