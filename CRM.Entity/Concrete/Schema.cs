using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entity.Concrete;

[Table("schema", Schema = "hangfire")]
public partial class Schema
{
	[Key]
	[Column("version")]
	public int Version { get; set; }
}
