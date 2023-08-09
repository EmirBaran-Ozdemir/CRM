using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entity.Concrete;

[Table("user_role")]
public partial class UserRole
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Column("name")]
	[StringLength(20)]
	public string Name { get; set; } = null!;

	[InverseProperty("Role")]
	public virtual ICollection<User> Users { get; set; } = new List<User>();
}
