using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using CRM.Business.ValidationRules;
using CRM.WebUI.Middleware;
using Serilog;
using Microsoft.Extensions.Configuration;

//. Builder Configurations
var builder = WebApplication.CreateBuilder(args);

// Log
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

//. Services
// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(p => p.LoginPath = "/Auth/Login");
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddControllersWithViews();
// Validators
builder.Services.AddValidatorsFromAssemblyContaining<RegisterVal>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginVal>();

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

//. Application
var app = builder.Build();
// Log
app.UseSerilogRequestLogging();
Log.Debug("App build with success");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Error/{0}");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseRouting();
//app.UseStatusCodePagesWithRedirects("/Error/{0}");
app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
Log.Debug("Terminating...");
Log.CloseAndFlush();