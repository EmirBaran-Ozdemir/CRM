using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entity.Concrete;

[Table("user")]
public partial class User
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Column("name")]
	[StringLength(50)]
	public string Name { get; set; } = null!;

	[Column("role_id")]
	public int RoleId { get; set; }

	[Column("company_id")]
	public int CompanyId { get; set; }

	[Column("quota")]
	public float Quota { get; set; }

	[Column("password")]
	[StringLength(200)]
	public string Password { get; set; } = null!;

	[Column("phone")]
	[StringLength(15)]
	public string Phone { get; set; } = null!;

	[Column("address")]
	[StringLength(200)]
	public string Address { get; set; } = null!;

	[Column("email")]
	[StringLength(50)]
	public string Email { get; set; } = null!;

	[Column("gender")]
	public short Gender { get; set; }

	[ForeignKey("CompanyId")]
	[InverseProperty("Users")]
	public virtual Company? Company { get; set; }

	[InverseProperty("User")]
	public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

	[InverseProperty("Customer")]
	public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

	[InverseProperty("Seller")]
	public virtual ICollection<Product> Products { get; set; } = new List<Product>();

	[ForeignKey("RoleId")]
	[InverseProperty("Users")]
	public virtual UserRole? Role { get; set; }
}
