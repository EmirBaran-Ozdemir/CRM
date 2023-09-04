using CRM.Business.Abstract;
using CRM.Business.ValidationRules;
using CRM.DataTypeObjects.Models;
using CRM.Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CRM.WebUI.Controllers
{
	[AllowAnonymous]

	public class CompanyController : Controller
	{
		readonly ICompanyService _companyManager;
		readonly IProductService _productManager;

		public CompanyController(ICompanyService companyManager, IProductService productManager)
		{
			_companyManager = companyManager;
			_productManager = productManager;
		}

		public IActionResult Index()
		{
			var model = _companyManager.GetListAll();
			ViewBag.IsAuthorized = HttpContext.User.IsInRole("admin");
			ViewBag.ProcessStatus = TempData.ContainsKey("ProcessStatus") && (bool)TempData["ProcessStatus"]!;
			ViewBag.ProcessMessage = TempData["ProcessMessage"] as string;
			return View(model);
		}

		public IActionResult Products(int id)
		{
			CompanyProductsModel model = new CompanyProductsModel();
			model.Company = _companyManager.GetById(id);
			List<User> users = _companyManager.GetCompanyEmployees(id);
			foreach (User user in users)
			{
				List<Product> userProducts = _productManager.GetAllProductsWithTypesBySellerId(user.Id);
				model.Products?.AddRange(userProducts);
			}
			return View("CompanyProducts", model);
		}

		public IActionResult Employees(int id)
		{
			CompanyEmployeesModel model = new CompanyEmployeesModel();
			model.Company = _companyManager.GetById(id);
			model.Users = _companyManager.GetUsersWithRolesByCompanyId(id);

			return View("CompanyEmployees", model);
		}

		[Authorize(Policy = "Admin")]
		public IActionResult AddCompany()
		{
			return View();
		}
		[HttpPost]
		public IActionResult AddCompany(Company model)
		{
			AddCompanyVal validator = new AddCompanyVal();
			ValidationResult results = validator.Validate(model);
			if (results.IsValid == false)
			{

				foreach (var failure in results.Errors)
				{
					ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
				}
				return View(model);
			}

			var existingCompany = _companyManager.CheckCompany(model.Name);
			if (existingCompany)
			{
				ModelState.AddModelError("Name", "This company already exists.");
				ViewBag.ProcessStatus = false;
				ViewBag.ProcessMessage = "Failed to add company.";
				return View();
			}
			_companyManager.Add(model);
			TempData["ProcessStatus"] = true;
			TempData["ProcessMessage"] = "Company added succesfully";
			return RedirectToAction("Index");
		}
	}
}
