using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entity.Concrete;

[Table("list", Schema = "hangfire")]
[Index("Expireat", Name = "ix_hangfire_list_expireat")]
public partial class List
{
	[Key]
	[Column("id")]
	public long Id { get; set; }

	[Column("key")]
	public string Key { get; set; } = null!;

	[Column("value")]
	public string? Value { get; set; }

	[Column("expireat")]
	public DateTime? Expireat { get; set; }

	[Column("updatecount")]
	public int Updatecount { get; set; }
}
