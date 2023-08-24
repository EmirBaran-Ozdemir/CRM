using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace CRM.WebUI.Middleware
{
	public class AuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
	{
		public async Task HandleAsync(
			RequestDelegate next,
			HttpContext context,
			AuthorizationPolicy policy,
			PolicyAuthorizationResult authorizeResult)
		{

			if (authorizeResult.Forbidden && !authorizeResult.Succeeded)
			{
				context.Response.StatusCode = StatusCodes.Status403Forbidden;

				throw new NotAuthorizedException($"You do not have the necessary permissions to access {context.Request.Path} page.");
			}
			await next.Invoke(context);
		}
	}

}