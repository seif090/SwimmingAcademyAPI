using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public partial class Time
{
    public long TID { get; set; }

    public long SwimmerID { get; set; }

    public short Site { get; set; }

    public short RaceID { get; set; }

    public decimal Time1 { get; set; }

    public short RacePlaceID { get; set; }

    public DateOnly RaceDate { get; set; }

    public int AddedBY { get; set; }

    public DateTime AddedAt { get; set; }

    public virtual user AddedBYNavigation { get; set; } = null!;

    public virtual AppCode Race { get; set; } = null!;

    public virtual AppCode RacePlace { get; set; } = null!;

    public virtual AppCode SiteNavigation { get; set; } = null!;

    public virtual Info2 Swimmer { get; set; } = null!;
}
