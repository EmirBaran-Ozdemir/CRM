using CRM.Business.Abstract;
using CRM.Business.Concrete;
using CRM.Business.ValidationRules;
using CRM.DataAccess.Abstract;
using CRM.DataAccess.EntityFramework;
using CRM.WebUI.Middleware;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Serilog;
using Hangfire;
using Hangfire.PostgreSql;
using CRM.API.Concrete;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Filters;
using CRM.WebUI.PackageConf;
using Microsoft.Extensions.Configuration;

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
	options.AddPolicy("Buyer", policy =>
	{
		policy.RequireRole("buyer", "admin");
	});
	options.AddPolicy("Seller", policy =>
	{
		policy.RequireRole("seller", "admin");
	});
});

// Add the AuthorizationMiddlewareResultHandler as a singleton
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultHandler>();

builder.Services.AddMvc(config =>
{
	var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
	config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddControllersWithViews();

// Controller Services
builder.Services.AddScoped<ICompanyService, CompanyManager>();
builder.Services.AddScoped<ICompanyDal, EFCompanyRepo>();

builder.Services.AddScoped<IInvoiceService, InvoiceManager>();
builder.Services.AddScoped<IInvoiceDal, EFInvoiceRepo>();

builder.Services.AddScoped<IMembershipService, MembershipManager>();
builder.Services.AddScoped<IMembershipDal, EFMembershipRepo>();

builder.Services.AddScoped<IOrderService, OrderManager>();
builder.Services.AddScoped<IOrderDal, EFOrderRepo>();

builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IProductDal, EFProductRepo>();

builder.Services.AddScoped<IProductTypeService, ProductTypeManager>();
builder.Services.AddScoped<IProductTypeDal, EFProductTypeRepo>();

builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IUserDal, EFUserRepo>();

builder.Services.AddScoped<IUserRoleService, UserRoleManager>();
builder.Services.AddScoped<IUserRoleDal, EFUserRoleRepo>();

builder.Services.AddScoped<IInvoiceGenerationService, InvoiceGenerationService>();


// Validators
builder.Services.AddValidatorsFromAssemblyContaining<AddCompanyVal>();
builder.Services.AddValidatorsFromAssemblyContaining<AddOrderVal>();
builder.Services.AddValidatorsFromAssemblyContaining<AddProductVal>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginVal>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterVal>();

builder.Services.AddTransient<NotFoundPageHandlerMiddleware>();
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

builder.Services.AddSingleton<HangfireJobScheduler>();

string connectionString = "Host=127.0.0.1;Port=5432;Username=postgres;Password=postgre;Database=CRM;";
GlobalConfiguration.Configuration.UsePostgreSqlStorage(connectionString);

builder.Services.AddHangfire(configuration => configuration
	.UsePostgreSqlStorage(connectionString));
builder.Services.AddHangfireServer();

//. Application
var app = builder.Build();
// Log
app.UseSerilogRequestLogging();
Log.Debug("App build with success");

//app.UseMiddleware<ViewExistenceMiddleware>();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseMiddleware<NotFoundPageHandlerMiddleware>();


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

//. Hangfire Configurations
var jobScheduler = app.Services.GetRequiredService<HangfireJobScheduler>();
jobScheduler.ScheduleJobs();

app.Run();
Log.Debug("Terminating...");
Log.CloseAndFlush();
