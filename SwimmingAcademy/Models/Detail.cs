using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public partial class Detail
{
    public int ID { get; set; }

    public long PTeamID { get; set; }

    public long SwimmerID { get; set; }

    public int CoachID { get; set; }

    public short SwimmerLevel { get; set; }

    public short LastStar { get; set; }

    public short site { get; set; }

    public string? Attendence { get; set; }

    public virtual Coach Coach { get; set; } = null!;

    public virtual Info PTeam { get; set; } = null!;

    public virtual Info2 Swimmer { get; set; } = null!;

    public virtual AppCode SwimmerLevelNavigation { get; set; } = null!;

    public virtual AppCode siteNavigation { get; set; } = null!;
}
