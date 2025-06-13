using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public partial class log2
{
    public long LID { get; set; }

    public long swimmerID { get; set; }

    public int ActionID { get; set; }

    public short createdAtsite { get; set; }

    public int CreatedBy { get; set; }

    public DateOnly CreatedAtDate { get; set; }

    public virtual Action Action { get; set; } = null!;

    public virtual user CreatedByNavigation { get; set; } = null!;

    public virtual AppCode createdAtsiteNavigation { get; set; } = null!;

    public virtual Info2 swimmer { get; set; } = null!;
}
