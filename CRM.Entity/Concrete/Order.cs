using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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

    [Column("invoice_id")]
    public int? InvoiceId { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Orders")]
    public virtual User Customer { get; set; } = null!;

    [ForeignKey("InvoiceId")]
    [InverseProperty("Orders")]
    public virtual Invoice? Invoice { get; set; }

    [InverseProperty("Order")]
    public virtual Lifetime? Lifetime { get; set; }

    [InverseProperty("Order")]
    public virtual Membership? Membership { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Orders")]
    public virtual Product Product { get; set; } = null!;
}
