using CRM.Business.Concrete;
using CRM.Business.ValidationRules;
using CRM.DataAccess.EntityFramework;
using CRM.Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CRM.DataTypeObjects.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CRM.WebUI.Controllers
{
	[AllowAnonymous]
	public class AuthController : Controller
	{
		readonly UserManager _userManager = new UserManager(new EFUserRepo());

		[HttpGet]
		public IActionResult MyProfile()
		{
			string identityId = HttpContext.User.Identity!.Name!;

			if (int.TryParse(identityId, out int id))
			{
				User user = _userManager.GetUserWithCompanyAndProductsById(id);

				if (user != null)
				{
					return View(user);
				}
			}

			return RedirectToAction("Login", "Auth");
		}
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Profile(int id)
		{

			User user = _userManager.GetUserWithCompanyById(id);

			if (user != null)
			{
				return View(user);
			}
			return RedirectToAction("Login", "Auth");
		}


		[AllowAnonymous]
		public IActionResult Register()
		{

			RegisterModel model = new RegisterModel();
			model.Companies = _userManager.GetCompanies();
			return View(model);
		}

		[AllowAnonymous]
		[HttpPost]
		public IActionResult Register(RegisterModel model)
		{
			RegisterVal validator = new RegisterVal();

			ValidationResult results = validator.Validate(model.User);
			if (results.IsValid == false)
			{

				foreach (var failure in results.Errors)
				{
					ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
				}
				return View(model);
			}

			var existingUser = _userManager.LoginUser(model.User);
			if (existingUser != null)
			{
				ModelState.AddModelError("Email", "This email is already registered.");
				return View(model);
			}
			_userManager.Add(model.User);
			return RedirectToAction("Login", "Auth");

		}
		[AllowAnonymous]
		public IActionResult Login()
		{
			return View();
		}
		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> Login(LoginModel model)
		{
			LoginVal validator = new LoginVal();
			ValidationResult results = validator.Validate(model);
			if (results.IsValid == false)
			{
				foreach (var failure in results.Errors)
				{
					ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
				}
				return View(model);
			}
			User userModel = new User();
			userModel.Email = model.Email;
			userModel.Password = model.Password;

			var information = _userManager.LoginUser(userModel);
			if (information != null)
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, information.Id.ToString()),
					new Claim(ClaimTypes.Email, information.Email )
				};
				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

				return RedirectToAction("Index", "Home");
			}

			ViewData["ValidateMessage"] = "Invalid Credentials";
			return View();

		}
		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}

		public IActionResult DivByZeroError()
		{
			int a = 0;
			int b = 10 / a;
			return View();
		}
	}
}
