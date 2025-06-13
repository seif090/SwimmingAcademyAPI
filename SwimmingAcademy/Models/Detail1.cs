using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public partial class Detail1
{
    public int ID { get; set; }

    public long SchoolID { get; set; }

    public long SwimmerID { get; set; }

    public int CoachID { get; set; }

    public short SwimmerLevel { get; set; }

    public short site { get; set; }

    public decimal? Attendence { get; set; }

    public virtual Coach Coach { get; set; } = null!;

    public virtual info1 School { get; set; } = null!;

    public virtual Info2 Swimmer { get; set; } = null!;

    public virtual AppCode SwimmerLevelNavigation { get; set; } = null!;

    public virtual AppCode siteNavigation { get; set; } = null!;
}
