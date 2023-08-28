using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CRM.Entity.Concrete;

[Table("job", Schema = "hangfire")]
[Index("Expireat", Name = "ix_hangfire_job_expireat")]
[Index("Statename", Name = "ix_hangfire_job_statename")]
public partial class Job
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("stateid")]
    public long? Stateid { get; set; }

    [Column("statename")]
    public string? Statename { get; set; }

    [Column("invocationdata", TypeName = "jsonb")]
    public string Invocationdata { get; set; } = null!;

    [Column("arguments", TypeName = "jsonb")]
    public string Arguments { get; set; } = null!;

    [Column("createdat")]
    public DateTime Createdat { get; set; }

    [Column("expireat")]
    public DateTime? Expireat { get; set; }

    [Column("updatecount")]
    public int Updatecount { get; set; }

    [InverseProperty("Job")]
    public virtual ICollection<Jobparameter> Jobparameters { get; set; } = new List<Jobparameter>();

    [InverseProperty("Job")]
    public virtual ICollection<State> States { get; set; } = new List<State>();
}
