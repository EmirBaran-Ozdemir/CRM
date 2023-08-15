//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.AspNetCore.Mvc.ViewEngines;
//using Microsoft.AspNetCore.Mvc;

//namespace CRM.WebUI
//{
//	public class CustomViewEngine : IViewEngine
//	{
//		private const string ViewExtension = ".custom"; // Custom file extension for views

//		public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
//		{
//			if (viewName == null)
//			{
//				throw new ArgumentNullException(nameof(viewName));
//			}

//			// Your logic to locate the view file
//			string viewPath = GetViewPath(context, viewName);

//			if (viewPath == null)
//			{
//				return ViewEngineResult.NotFound(viewName, new List<string>());
//			}

//			return ViewEngineResult.Found(viewName, new CustomView(viewPath));
//		}

//		public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
//		{
//			// Since this is a simple example, we ignore executingFilePath and isMainPage
//			// Your logic to construct the full view path
//			string fullPath = Path.Combine(viewPath, $"{viewPath}{ViewExtension}");

//			if (File.Exists(fullPath))
//			{
//				return ViewEngineResult.Found(viewPath, new CustomView(fullPath));
//			}

//			return ViewEngineResult.NotFound(viewPath, new List<string>());
//		}

//		public class CustomView : IView
//		{
//			private readonly string _viewPath;

//			public CustomView(string viewPath)
//			{
//				_viewPath = viewPath;
//			}

//			public async Task RenderAsync(ViewContext context)
//			{
//				// Read the content of the view file and write it to the response stream
//				string viewContent = File.ReadAllText(_viewPath);
//				await context.Writer.WriteAsync(viewContent);
//			}
//		}
//	}
