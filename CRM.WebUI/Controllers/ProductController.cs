using CRM.API.Concrete;
using CRM.Business.Abstract;
using CRM.Business.ValidationRules;
using CRM.DataTypeObjects.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebUI.Controllers
{
	public class ProductController : Controller
	{

		readonly IProductService _productManager;
		readonly IUserService _userManager;

		public ProductController(IProductService productService, IUserService userService)
		{
			_productManager = productService;
			_userManager = userService;
		}
		[AllowAnonymous]
		public IActionResult Index()
		{
			var model = _productManager.GetAllWithCompanyAndProductType();
			ViewBag.CanSell = HttpContext.User.IsInRole("admin") || HttpContext.User.IsInRole("seller");
			ViewBag.CanBuy = HttpContext.User.IsInRole("admin") || HttpContext.User.IsInRole("buyer");
			ViewBag.ProcessStatus = TempData.ContainsKey("ProcessStatus") && (bool)TempData["ProcessStatus"]!;
			ViewBag.ProcessMessage = TempData["ProcessMessage"] as string;
			ProductIndexModel fullModel = new ProductIndexModel();
			fullModel.Products = model;
			fullModel.CreateOrderModel = new CreateOrderModel();

			return View(fullModel);
		}
		[Authorize(Policy = "Seller")]
		public IActionResult AddProduct()
		{
			return View();
		}
		[HttpPost]
		[Authorize(Policy = "Seller")]
		public IActionResult AddProduct(AddProductModel model)
		{
			var license = LicenseGenerator.GenerateLicense();

			while (_productManager.CheckSameLicense(license))
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
				ViewBag.ProcessStatus = false;
				ViewBag.ProcessMessage = "Failed to add product";
				return View(model);
			}

			if (_productManager.CheckSameProduct(model.Product.Name))
			{
				ModelState.AddModelError("Product.Name", "This product is already exist in shop.");
				ViewBag.ProcessStatus = false;
				ViewBag.ProcessMessage = "This product is already exist in shop.";
				return View(model);
			}
			_productManager.Add(model.Product);
			TempData["ProcessStatus"] = true;
			TempData["ProcessMessage"] = "Product added succesfully.";
			return RedirectToAction("Index");
		}

		[AllowAnonymous]
		public IActionResult Product(int id)
		{
			var model = _productManager.GetProductWithSellerInfoAndProductTypeById(id);
			return View("GetProductById", model);
		}
	}
}
