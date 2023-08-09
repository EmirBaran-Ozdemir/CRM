using CRM.Business.Concrete;
using CRM.DataAccess.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebUI.Controllers
{
	[AllowAnonymous]
	public class CompanyController : Controller
	{
		CompanyManager _companyManager = new CompanyManager(new EFCompanyRepo());
		public IActionResult Index()
		{
			var values = _companyManager.GetListAll();
			return View(values);
		}
		public IActionResult CompanyProducts(int id)
		{
			var values = _companyManager.GetUsersWithProductsByCompanyId(id);
			return View(values);
		}
		public IActionResult CompanyEmployees(int id)
		{
			var values = _companyManager.GetUsersWithRolesByCompanyId(id);
			return View(values);
		}
	}
}
