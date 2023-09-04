using CRM.Business.Abstract;
using CRM.Business.ValidationRules;
using CRM.DataTypeObjects.Models;
using CRM.Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRM.WebUI.Controllers
{
	[AllowAnonymous]
	public class AuthController : Controller
	{
		readonly IUserService _userManager;

		public AuthController(IUserService userManager)
		{
			_userManager = userManager;
		}

		[HttpGet]
		[Authorize]
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

			TempData["ProcessStatus"] = false;
			TempData["ProcessMessage"] = "You must be logged in.";
			return RedirectToAction("Login", "Auth");
		}
		[HttpGet]

		public IActionResult Profile(int id)
		{
			User user = _userManager.GetUserWithCompanyById(id);
			return View(user);
		}


		public IActionResult Register()
		{

			RegisterModel model = new RegisterModel();
			model.Companies = _userManager.GetCompanies();
			return View(model);
		}

		[HttpPost]
		public IActionResult Register(RegisterModel model)
		{
			RegisterVal validator = new RegisterVal();
			ValidationResult results = validator.Validate(model.User);
			model.Companies = _userManager.GetCompanies();
			if (results.IsValid == false)
			{

				foreach (var failure in results.Errors)
				{
					ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
				}
				ViewBag.ProcessStatus = false;
				ViewBag.ProcessMessage = "Registration failed.";
				return View(model);
			}

			var existingUser = _userManager.LoginUser(model.User);
			if (existingUser != null)
			{
				ModelState.AddModelError("Email", "This email is already registered.");
				ViewBag.ProcessStatus = false;
				ViewBag.ProcessMessage = "This email is already registered.";
				return View(model);
			}
			_userManager.Add(model.User);
			TempData["ProcessStatus"] = true;
			TempData["ProcessMessage"] = "Register successful.";
			return RedirectToAction("Login", "Auth");

		}
		public IActionResult Login()
		{
			return View();
		}
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
				ViewBag.ProcessStatus = false;
				ViewBag.ProcessMessage = "Login failed.";
				return View(model);
			}
			User userModel = new User();
			userModel.Email = model.Email;
			userModel.Password = model.Password;

			var information = _userManager.LoginUser(userModel);
			if (information == null)
			{
				ViewData["ValidateMessage"] = "Invalid Credentials.";
				ViewBag.ProcessStatus = false;
				ViewBag.ProcessMessage = "Login failed.";
				return View();
			}
			var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, information.Id.ToString()),
					new Claim(ClaimTypes.Email, information.Email ),
					new Claim(ClaimTypes.Role, information.Role!.Name.ToString()!)
				};
			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(identity);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

			TempData["ProcessStatus"] = true;
			TempData["ProcessMessage"] = "Login successful.";
			return RedirectToAction("Index", "Home");

		}
		[HttpGet]
		[Authorize]
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
