using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entity.Concrete;

[Table("jobparameter", Schema = "hangfire")]
[Index("Jobid", "Name", Name = "ix_hangfire_jobparameter_jobidandname")]
public partial class Jobparameter
{
	[Key]
	[Column("id")]
	public long Id { get; set; }

	[Column("jobid")]
	public long Jobid { get; set; }

	[Column("name")]
	public string Name { get; set; } = null!;

	[Column("value")]
	public string? Value { get; set; }

	[Column("updatecount")]
	public int Updatecount { get; set; }

	[ForeignKey("Jobid")]
	[InverseProperty("Jobparameters")]
	public virtual Job Job { get; set; } = null!;
}
