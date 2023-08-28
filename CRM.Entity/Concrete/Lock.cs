using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CRM.Entity.Concrete;

[Keyless]
[Table("lock", Schema = "hangfire")]
[Index("Resource", Name = "lock_resource_key", IsUnique = true)]
public partial class Lock
{
    [Column("resource")]
    public string Resource { get; set; } = null!;

    [Column("updatecount")]
    public int Updatecount { get; set; }

    [Column("acquired")]
    public DateTime? Acquired { get; set; }
}
