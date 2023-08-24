using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entity.Concrete;

[Table("lifetime")]
[Index("OrderId", Name = "lifetime_order_id_key", IsUnique = true)]
public partial class Lifetime
{
	[Key]
	[Column("order_id")]
	public int OrderId { get; set; }

	[Column("payment_collected")]
	public bool PaymentCollected { get; set; }

	[ForeignKey("OrderId")]
	[InverseProperty("Lifetime")]
	public virtual Order Order { get; set; } = null!;
}
