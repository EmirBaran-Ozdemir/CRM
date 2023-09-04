using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entity.Concrete;

[Table("jobqueue", Schema = "hangfire")]
[Index("Jobid", "Queue", Name = "ix_hangfire_jobqueue_jobidandqueue")]
[Index("Queue", "Fetchedat", Name = "ix_hangfire_jobqueue_queueandfetchedat")]
[Index("Queue", "Fetchedat", "Jobid", Name = "jobqueue_queue_fetchat_jobid")]
public partial class Jobqueue
{
	[Key]
	[Column("id")]
	public long Id { get; set; }

	[Column("jobid")]
	public long Jobid { get; set; }

	[Column("queue")]
	public string Queue { get; set; } = null!;

	[Column("fetchedat")]
	public DateTime? Fetchedat { get; set; }

	[Column("updatecount")]
	public int Updatecount { get; set; }
}
