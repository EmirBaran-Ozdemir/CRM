using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Entity.Concrete;

[Table("server", Schema = "hangfire")]
public partial class Server
{
	[Key]
	[Column("id")]
	public string Id { get; set; } = null!;

	[Column("data", TypeName = "jsonb")]
	public string? Data { get; set; }

	[Column("lastheartbeat")]
	public DateTime Lastheartbeat { get; set; }

	[Column("updatecount")]
	public int Updatecount { get; set; }
}
