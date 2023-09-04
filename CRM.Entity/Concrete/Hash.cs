using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entity.Concrete;

[Table("hash", Schema = "hangfire")]
[Index("Key", "Field", Name = "hash_key_field_key", IsUnique = true)]
[Index("Expireat", Name = "ix_hangfire_hash_expireat")]
public partial class Hash
{
	[Key]
	[Column("id")]
	public long Id { get; set; }

	[Column("key")]
	public string Key { get; set; } = null!;

	[Column("field")]
	public string Field { get; set; } = null!;

	[Column("value")]
	public string? Value { get; set; }

	[Column("expireat")]
	public DateTime? Expireat { get; set; }

	[Column("updatecount")]
	public int Updatecount { get; set; }
}
