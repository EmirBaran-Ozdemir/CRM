using CRM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataTypeObjects.Models
{
	public class CompanyEmployeesModel
	{
		public Company? Company { get; set; }
		public List<User> Users { get; set; } = new List<User>();
	}
}
