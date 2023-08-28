using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CRM.Entity.Concrete;

[Table("counter", Schema = "hangfire")]
[Index("Expireat", Name = "ix_hangfire_counter_expireat")]
[Index("Key", Name = "ix_hangfire_counter_key")]
public partial class Counter
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
