using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using Serilog;
using Serilog.Context;

namespace CRM.WebUI.Middleware
{
	public class GlobalExceptionHandlingMiddleware : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				using (LogContext.PushProperty("RequestName", typeof(RequestDelegate).Name))
				using (LogContext.PushProperty("DateTimeUtc", DateTime.UtcNow))
				{
					Log.Information("Starting request");
				}

				await next.Invoke(context);
			}
			catch (Exception ex)
			{
				using (LogContext.PushProperty("RequestName", typeof(Exception).Name))
				using (LogContext.PushProperty("Error", ex))
				using (LogContext.PushProperty("DateTimeUtc", DateTime.UtcNow))
				{
					Log.Error(ex, "Request failure");

					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

					//context.Response.ContentType = "application/problem+json";
					ProblemDetails problem = new ProblemDetails()
					{
						Status = (int)HttpStatusCode.InternalServerError,
						Type = "Server Error",
						Title = "Server Error",
						Detail = ex.Message,
					};
					Log.Fatal(problem.Detail);
					context.Response.Redirect($"/Error/{context.Response.StatusCode}");
					//string json = JsonSerializer.Serialize(problem);
					//await context.Response.WriteAsync(json);
				}
			}
		}
	}
}