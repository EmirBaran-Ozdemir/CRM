namespace CRM.WebUI.Middleware
{
	public class NotFoundPageHandlerMiddleware : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			await next.Invoke(context);
			if (context.Response.StatusCode == 404)
			{
				throw new NotFoundException($"Page {context.Request.Path} not found");
			}
		}
	}
}
