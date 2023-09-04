using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entity.Concrete;

[Table("Invoice")]
public partial class Invoice
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Column("user_id")]
	public int UserId { get; set; }

	[Column("excess_amount")]
	public float ExcessAmount { get; set; }

	[Column("invoince_start_date")]
	public DateOnly InvoinceStartDate { get; set; }

	[Column("invoince_end_date")]
	public DateOnly InvoinceEndDate { get; set; }

	[InverseProperty("Invoice")]
	public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

	[ForeignKey("UserId")]
	[InverseProperty("Invoices")]
	public virtual User User { get; set; } = null!;
}
