﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entity.Concrete;

[Table("order")]
public partial class Order
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Column("customer_id")]
	public int CustomerId { get; set; }

	[Column("product_id")]
	public int ProductId { get; set; }

	[Column("order_date")]
	public DateOnly OrderDate { get; set; }

	[Column("current_price")]
	public float CurrentPrice { get; set; }

	[ForeignKey("CustomerId")]
	[InverseProperty("Orders")]
	public virtual User Customer { get; set; } = null!;

	[InverseProperty("Order")]
	public virtual Lifetime? Lifetime { get; set; }

	[InverseProperty("Order")]
	public virtual Membership? Membership { get; set; }

	[InverseProperty("Order")]
	public virtual ICollection<OrderInvoiceMap> OrderInvoiceMaps { get; set; } = new List<OrderInvoiceMap>();

	[ForeignKey("ProductId")]
	[InverseProperty("Orders")]
	public virtual Product Product { get; set; } = null!;
}
