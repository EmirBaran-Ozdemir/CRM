using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entity.Concrete;

[Table("aggregatedcounter", Schema = "hangfire")]
[Index("Key", Name = "aggregatedcounter_key_key", IsUnique = true)]
public partial class Aggregatedcounter
{
	[Key]
	[Column("id")]
	public long Id { get; set; }

	[Column("key")]
	public string Key { get; set; } = null!;

	[Column("value")]
	public long Value { get; set; }

	[Column("expireat")]
	public DateTime? Expireat { get; set; }
}
