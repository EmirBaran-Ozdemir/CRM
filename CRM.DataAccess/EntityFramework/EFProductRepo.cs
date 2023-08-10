using CRM.DataAccess.Abstract;
using CRM.DataAccess.Concrete;
using CRM.DataAccess.Repository;
using CRM.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess.EntityFramework
{
	public class EFProductRepo : GenericRepo<Product>, IProductDal
	{
		CRMContext context = new CRMContext();
		public List<Product> GetAllProductsWithTypesByTypeId(int id)
		{
			return context.Products.Include(x => x.ProductType).Where(x => x.ProductTypeId == id).ToList();
		}
		
		public Product GetProductWithSellerInfoAndProductTypeById(int id)
		{
			return context.Products.Include(x => x.Seller)
				.Include(x => x.Seller!.Company)
				.Include(x => x.ProductType)
				.FirstOrDefault(x => x.Id == id)!;
		}
		public List<Product> GetAllProductsWithTypesBySellerId(int id)
		{
			return context.Products.Include(x => x.ProductType).Where(x => x.SellerId == id).ToList();
		}
		public List<Product> GetAllWithCompanyAndProductType()
		{
			return context.Products.Include(x => x.Seller!.Company).Include(x => x.ProductType).ToList();
		}
		public bool CheckSameProduct(string name)
		{
			return context.Products.Any(x => x.Name == name);
		}
		public bool CheckSameLicense(string license)
		{
			return context.Products.Any(x => x.License == license);
		}
	}
}
