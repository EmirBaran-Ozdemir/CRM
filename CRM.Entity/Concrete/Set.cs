using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entity.Concrete;

[Table("set", Schema = "hangfire")]
[Index("Expireat", Name = "ix_hangfire_set_expireat")]
[Index("Key", "Score", Name = "ix_hangfire_set_key_score")]
[Index("Key", "Value", Name = "set_key_value_key", IsUnique = true)]
public partial class Set
{
	[Key]
	[Column("id")]
	public long Id { get; set; }

	[Column("key")]
	public string Key { get; set; } = null!;

	[Column("score")]
	public double Score { get; set; }

	[Column("value")]
	public string Value { get; set; } = null!;

	[Column("expireat")]
	public DateTime? Expireat { get; set; }

	[Column("updatecount")]
	public int Updatecount { get; set; }
}
