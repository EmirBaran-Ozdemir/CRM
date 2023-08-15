﻿using CRM.WebUI.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Serilog;

namespace CRM.WebUI.Middleware
{
	public class AuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
	{
		private readonly GlobalExceptionHandlerMiddleware globalExceptionHandlerMiddleware = new();

		public async Task HandleAsync(
			RequestDelegate next,
			HttpContext context,
			AuthorizationPolicy policy,
			PolicyAuthorizationResult authorizeResult)
		{

			if (authorizeResult.Forbidden)
			{
				context.Response.StatusCode = StatusCodes.Status403Forbidden;

				throw new NotAuthorizedException($"You do not have the necessary permissions to access this page.");
			}
			await globalExceptionHandlerMiddleware.InvokeAsync(context, next);
		}
	}
	
}