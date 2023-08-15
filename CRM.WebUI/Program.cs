using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using CRM.Business.ValidationRules;
using CRM.WebUI.Middleware;
using Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

//. Builder Configurations
var builder = WebApplication.CreateBuilder(args);

// Log
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

//. Services
// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(p => p.LoginPath = "/Auth/Login");

// Authorization
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("Admin", policy =>
	{
		policy.RequireRole("admin");
	});
});

// Add the AuthorizationMiddlewareResultHandler as a singleton
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultHandler>();

builder.Services.AddMvc(config =>
{
	//var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
	//config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddControllersWithViews();

// Validators
builder.Services.AddValidatorsFromAssemblyContaining<RegisterVal>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginVal>();
builder.Services.AddValidatorsFromAssemblyContaining<AddProductVal>();

builder.Services.AddTransient<ViewExistenceMiddleware>();
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

//. Application
var app = builder.Build();
// Log
app.UseSerilogRequestLogging();
Log.Debug("App build with success");

//app.UseMiddleware<ViewExistenceMiddleware>();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
Log.Debug("Terminating...");
Log.CloseAndFlush();
