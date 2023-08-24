using CRM.Entity.Concrete;

namespace CRM.DataTypeObjects.Models
{
	public class CompanyEmployeesModel
	{
		public Company? Company { get; set; }
		public List<User> Users { get; set; } = new List<User>();
	}
}
