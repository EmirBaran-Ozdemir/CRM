using CRM.API.Concrete;
using CRM.Business.Concrete;
using CRM.Business.ValidationRules;
using CRM.DataAccess.EntityFramework;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CRM.DataTypeObjects.Models;

namespace CRM.WebUI.Controllers
{
    [AllowAnonymous]
	public class ProductController : Controller
	{

		readonly ProductManager _productManager = new ProductManager(new EFProductRepo());
		readonly UserManager _userManager = new UserManager(new EFUserRepo());
		readonly CompanyManager _companyManager = new CompanyManager(new EFCompanyRepo());
		readonly ProductTypeManager _productTypeManager = new ProductTypeManager(new EFProductTypeRepo());
		public IActionResult Index()
		{
			var values = _productManager.GetListAll();
			return View(values);
		}
		public IActionResult AddProduct()
		{
			return View();
		}
		[HttpPost]
		public IActionResult AddProduct(AddProductModel model)
		{
			var license = LicenseGenerator.GenerateLicense();

			while(!_productManager.CheckSameLicense(license))
				license = LicenseGenerator.GenerateLicense();

			model.User = _userManager.GetUserWithCompanyById(int.Parse(HttpContext.User.Identity!.Name!));
			model.Product.License = license;
			model.Product.SellerId = model.User.Id;

			AddProductVal addProductVal = new AddProductVal();
			ValidationResult results = addProductVal.Validate(model.Product);

			if (results.IsValid == false)
			{
				foreach (var failure in results.Errors)
				{
					ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
				}
				return View(model);
			}

			if (_productManager.CheckSameProduct(model.Product.Name))
			{
				ModelState.AddModelError("Product.Name", "This product is already exist in shop.");
				return View(model);
			}
			_productManager.Add(model.Product);
			return RedirectToAction("Index");
		}
		public IActionResult Product(int id)
		{
			GetProductModel getProductModel = new GetProductModel();
			getProductModel.Product = _productManager.GetById(id);
			getProductModel.User = _userManager.GetUserWithCompanyById(getProductModel.Product.SellerId);
			getProductModel.Company = _companyManager.GetById(getProductModel.User.CompanyId);
			getProductModel.ProductType = _productManager.GetProductTypeById(getProductModel.Product.ProductTypeId);
			return View("GetProductById", getProductModel);
		}
	}
}
