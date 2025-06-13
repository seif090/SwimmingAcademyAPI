using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public partial class log
{
    public long LID { get; set; }

    public long PteamID { get; set; }

    public int ActionID { get; set; }

    public long? SwimmerID { get; set; }

    public short CreatedAtSite { get; set; }

    public int createdby { get; set; }

    public DateOnly createdAtDate { get; set; }

    public virtual Action Action { get; set; } = null!;

    public virtual AppCode CreatedAtSiteNavigation { get; set; } = null!;

    public virtual Info Pteam { get; set; } = null!;

    public virtual Info2? Swimmer { get; set; }

    public virtual user createdbyNavigation { get; set; } = null!;
}
