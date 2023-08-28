using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CRM.Entity.Concrete;

[Table("state", Schema = "hangfire")]
[Index("Jobid", Name = "ix_hangfire_state_jobid")]
public partial class State
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("jobid")]
    public long Jobid { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("reason")]
    public string? Reason { get; set; }

    [Column("createdat")]
    public DateTime Createdat { get; set; }

    [Column("data", TypeName = "jsonb")]
    public string? Data { get; set; }

    [Column("updatecount")]
    public int Updatecount { get; set; }

    [ForeignKey("Jobid")]
    [InverseProperty("States")]
    public virtual Job Job { get; set; } = null!;
}
