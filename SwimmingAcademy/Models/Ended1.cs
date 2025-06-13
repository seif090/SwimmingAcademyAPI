using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public partial class Ended1
{
    public long Schoolid { get; set; }

    public DateOnly EndedAt { get; set; }

    public int EndedBy { get; set; }

    public virtual user EndedByNavigation { get; set; } = null!;

    public virtual info1 School { get; set; } = null!;
}
