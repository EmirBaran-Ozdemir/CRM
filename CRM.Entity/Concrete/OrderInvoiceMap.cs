using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entity.Concrete;

[Table("orderInvoiceMap")]
public partial class OrderInvoiceMap
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Column("invoice_id")]
	public int InvoiceId { get; set; }

	[Column("order_id")]
	public int OrderId { get; set; }

	[ForeignKey("InvoiceId")]
	[InverseProperty("OrderInvoiceMaps")]
	public virtual Invoice Invoice { get; set; } = null!;

	[ForeignKey("OrderId")]
	[InverseProperty("OrderInvoiceMaps")]
	public virtual Order Order { get; set; } = null!;
}
