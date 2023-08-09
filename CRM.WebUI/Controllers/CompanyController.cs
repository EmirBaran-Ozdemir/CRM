using CRM.Business.Concrete;
using CRM.DataAccess.EntityFramework;
using CRM.DataTypeObjects.Models;
using CRM.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebUI.Controllers
{
	public class CompanyController : Controller
	{
		readonly CompanyManager _companyManager = new CompanyManager(new EFCompanyRepo());
		readonly ProductManager _productManager = new ProductManager(new EFProductRepo());
		
		public IActionResult Index()
		{
			var model = _companyManager.GetListAll();
			return View(model);
		}

		public IActionResult Products(int id)
		{
			CompanyProductsModel model = new CompanyProductsModel();
			model.Company = _companyManager.GetById(id);
			List<User> users = _companyManager.GetCompanyEmployees(id);
			foreach(User user in users)
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
	}
}
