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
builder.Services.AddTransient<ICompanyService, CompanyManager>();
builder.Services.AddTransient<ICompanyDal, EFCompanyRepo>();

builder.Services.AddTransient<IInvoiceService, InvoiceManager>();
builder.Services.AddTransient<IInvoiceDal, EFInvoiceRepo>();

builder.Services.AddTransient<IMembershipService, MembershipManager>();
builder.Services.AddTransient<IMembershipDal, EFMembershipRepo>();

builder.Services.AddTransient<IOrderService, OrderManager>();
builder.Services.AddTransient<IOrderDal, EFOrderRepo>();

builder.Services.AddTransient<IProductService, ProductManager>();
builder.Services.AddTransient<IProductDal, EFProductRepo>();

builder.Services.AddTransient<IProductTypeService, ProductTypeManager>();
builder.Services.AddTransient<IProductTypeDal, EFProductTypeRepo>();

builder.Services.AddTransient<IUserService, UserManager>();
builder.Services.AddTransient<IUserDal, EFUserRepo>();

builder.Services.AddTransient<IUserRoleService, UserRoleManager>();
builder.Services.AddTransient<IUserRoleDal, EFUserRoleRepo>();

// Validators
builder.Services.AddValidatorsFromAssemblyContaining<AddCompanyVal>();
builder.Services.AddValidatorsFromAssemblyContaining<AddOrderVal>();
builder.Services.AddValidatorsFromAssemblyContaining<AddProductVal>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginVal>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterVal>();

builder.Services.AddTransient<NotFoundPageHandlerMiddleware>();
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

//. Application
var app = builder.Build();
// Log
app.UseSerilogRequestLogging();
Log.Debug("App build with success");

//string connectionString = "Host=127.0.0.1;Port=5432;Username=postgres;Password=postgre;Database=CRM;";
//builder.Services.AddHangfire(config =>
//{
//	config.UsePostgreSqlStorage(connectionString);
//});
//app.UseHangfireDashboard();


UserManager userManager = new UserManager(new EFUserRepo());
OrderManager orderManager = new OrderManager(new EFOrderRepo());
InvoiceManager ýnvoiceManager = new InvoiceManager(new EFInvoiceRepo());


//InvoiceGenerator invoiceGenerator = new InvoiceGenerator(userManager, orderManager, ýnvoiceManager);
//RecurringJob.AddOrUpdate<InvoiceGenerator>(
//	"monthly_invoice_generation",
//	invoiceGenerator => invoiceGenerator.GenerateInvoices(),
//	Cron.Minutely);


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

app.Run();
Log.Debug("Terminating...");
Log.CloseAndFlush();
