using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using System.Net;

namespace CRM.WebUI.Middleware
{
	public class GlobalExceptionHandlerMiddleware : IMiddleware
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
			catch (CodedException ex)
			{
				using (LogContext.PushProperty("RequestName", typeof(Exception).Name))
				using (LogContext.PushProperty("Error", ex))
				using (LogContext.PushProperty("DateTimeUtc", DateTime.UtcNow))
				{
					Log.Error(ex, "Request failure");

					ProblemDetails problem = setProblem(ex.Code, "Server Error", ex.Message);
					Log.Fatal(problem.Detail!);

					var errorString = $"?message={WebUtility.UrlEncode(ex.Message)}&code={ex.Code}";
					context.Response.Redirect($"/Error{errorString}");
				}
			}
			catch (Exception ex)
			{
				using (LogContext.PushProperty("RequestName", typeof(Exception).Name))
				using (LogContext.PushProperty("Error", ex))
				using (LogContext.PushProperty("DateTimeUtc", DateTime.UtcNow))
				{
					Log.Error(ex, "Request failure");

					int errCode = 500;
					ProblemDetails problem = setProblem(errCode, "Server Error", ex.Message);
					Log.Fatal(problem.Detail!);

					var errorString = $"?message=Server Error Occured&code={errCode}";
					context.Response.Redirect($"/Error{errorString}");
				}
			}
		}

		private ProblemDetails setProblem(int errorCode, string errorType, string exceptionMessage)
		{
			return new ProblemDetails()
			{
				Status = errorCode,
				Type = errorType,
				Detail = exceptionMessage,
			};
		}
	}
}
