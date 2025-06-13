using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public partial class Ended
{
    public long PTeamID { get; set; }

    public DateOnly EndedAt { get; set; }

    public int EndedBy { get; set; }

    public virtual user EndedByNavigation { get; set; } = null!;

    public virtual Info PTeam { get; set; } = null!;
}
