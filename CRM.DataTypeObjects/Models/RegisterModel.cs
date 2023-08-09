using CRM.Entity.Concrete;

namespace CRM.DataTypeObjects.Models
{
    public class RegisterModel
    {
        public User User { get; set; } = null!;
        public List<Company>? Companies { get; set; }

    }
}

