using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class ActivityLog
{
    public int LogId { get; set; }

    public int? UserId { get; set; }

    public string? Action { get; set; }

    public DateTime? LogDate { get; set; }

    public virtual User? User { get; set; }
}
