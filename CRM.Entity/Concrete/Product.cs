using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entity.Concrete;

[Table("product")]
public partial class Product
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Column("name")]
	[StringLength(100)]
	public string Name { get; set; } = null!;

	[Column("seller_id")]
	public int SellerId { get; set; }

	[Column("product_type_id")]
	public int ProductTypeId { get; set; }

	[Column("price")]
	public float Price { get; set; }

	[Column("license")]
	[StringLength(50)]
	public string? License { get; set; }

	[Column("details", TypeName = "character varying")]
	public string? Details { get; set; }

	[InverseProperty("Product")]
	public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();

	[ForeignKey("ProductTypeId")]
	[InverseProperty("Products")]
	public virtual ProductType? ProductType { get; set; } = null!;

	[ForeignKey("SellerId")]
	[InverseProperty("Products")]
	public virtual User? Seller { get; set; } = null!;
}
