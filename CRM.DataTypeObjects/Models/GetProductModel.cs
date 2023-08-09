using CRM.Entity.Concrete;


namespace CRM.DataTypeObjects.Models
{
    public class GetProductModel
    {
        public Product Product { get; set; } = null!;
        public User User { get; set; } = null!;
        public Company Company { get; set; } = null!;
        public ProductType ProductType { get; set; } = null!;
    }
}
