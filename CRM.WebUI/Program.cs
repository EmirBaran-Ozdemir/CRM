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
builder.Services.AddSingleton<ICompanyService, CompanyManager>();
builder.Services.AddSingleton<ICompanyDal, EFCompanyRepo>();

builder.Services.AddSingleton<IInvoiceService, InvoiceManager>();
builder.Services.AddSingleton<IInvoiceDal, EFInvoiceRepo>();

builder.Services.AddSingleton<IMembershipService, MembershipManager>();
builder.Services.AddSingleton<IMembershipDal, EFMembershipRepo>();

builder.Services.AddSingleton<IOrderService, OrderManager>();
builder.Services.AddSingleton<IOrderDal, EFOrderRepo>();

builder.Services.AddSingleton<IProductService, ProductManager>();
builder.Services.AddSingleton<IProductDal, EFProductRepo>();

builder.Services.AddSingleton<IProductTypeService, ProductTypeManager>();
builder.Services.AddSingleton<IProductTypeDal, EFProductTypeRepo>();

builder.Services.AddSingleton<IUserService, UserManager>();
builder.Services.AddSingleton<IUserDal, EFUserRepo>();

builder.Services.AddSingleton<IUserRoleService, UserRoleManager>();
builder.Services.AddSingleton<IUserRoleDal, EFUserRoleRepo>();

builder.Services.AddScoped<IInvoiceGenerationService, InvoiceGenerationService>();


// Validators
builder.Services.AddValidatorsFromAssemblyContaining<AddCompanyVal>();
builder.Services.AddValidatorsFromAssemblyContaining<AddOrderVal>();
builder.Services.AddValidatorsFromAssemblyContaining<AddProductVal>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginVal>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterVal>();

builder.Services.AddTransient<NotFoundPageHandlerMiddleware>();
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

string connectionString = "Host=127.0.0.1;Port=5432;Username=postgres;Password=postgre;Database=CRM;";
builder.Services.AddHangfire(config =>
{
	config.UsePostgreSqlStorage(connectionString);
});

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

//. Hangfire Configurations
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{

});
using (var scope = app.Services.CreateScope())
{
	var invoiceGenerationService = scope.ServiceProvider.GetRequiredService<IInvoiceGenerationService>();
	var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();

	recurringJobManager.AddOrUpdate(
		"monthly_invoice_generation",
		() => invoiceGenerationService.GenerateInvoices(),
		Cron.Minutely);
}

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
Log.Debug("Terminating...");
Log.CloseAndFlush();
