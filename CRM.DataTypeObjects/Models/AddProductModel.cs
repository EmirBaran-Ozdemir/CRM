using CRM.Entity.Concrete;

namespace CRM.DataTypeObjects.Models
{
    public class AddProductModel
    {
        public User User { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
