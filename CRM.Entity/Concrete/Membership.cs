using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entity.Concrete;

[Table("membership")]
[Index("OrderId", Name = "membership_order_id_key", IsUnique = true)]
public partial class Membership
{
	[Key]
	[Column("order_id")]
	public int OrderId { get; set; }

	[Column("start_date")]
	public DateOnly StartDate { get; set; }

	[Column("end_date")]
	public DateOnly EndDate { get; set; }

	[Column("payment_collected")]
	public bool PaymentCollected { get; set; }

	[ForeignKey("OrderId")]
	[InverseProperty("Membership")]
	public virtual Order Order { get; set; } = null!;
}
