using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entity.Concrete;

[Table("membership")]
public partial class Membership
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Column("order_id")]
	public int OrderId { get; set; }

	[Column("start_date")]
	public DateOnly StartDate { get; set; }

	[Column("end_date")]
	public DateOnly EndDate { get; set; }

	[ForeignKey("OrderId")]
	[InverseProperty("Memberships")]
	public virtual Order Order { get; set; } = null!;
}
