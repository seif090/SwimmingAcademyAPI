using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public class Technical
{
    public long SwimmerID { get; set; }
    public short Site { get; set; }
    public short CurrentLevel { get; set; }
    public bool? ISSchool { get; set; }
    public bool? ISPreTeam { get; set; }
    public short? LastStar { get; set; }
    public bool? ISTeam { get; set; }
    public bool? IsShort { get; set; }
    public short? FirstSP { get; set; }
    public short? SecondSP { get; set; }
    public bool? IsIM { get; set; }
    public bool? IsFs { get; set; }
    public bool? IsLong { get; set; }
    public bool? IsFins { get; set; }
    public bool? IsMono { get; set; }
    public int Addedby { get; set; }
    public DateOnly AddedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateOnly? UpdatedAt { get; set; }
    public DateOnly? ExpiryDate { get; set; }
    public virtual user AddedbyNavigation { get; set; } = null!; // Fix: Initialize with null-forgiving operator
    public virtual AppCode CurrentLevelNavigation { get; set; } = null!; // Fix: Initialize with null-forgiving operator
    public virtual AppCode? FirstSPNavigation { get; set; }
    public virtual AppCode? LastStarNavigation { get; set; }
    public virtual AppCode? SecondSPNavigation { get; set; }
    public virtual AppCode SiteNavigation { get; set; } = null!; // Fix: Initialize with null-forgiving operator
    public virtual Info2 Swimmer { get; set; } = null!; // Fix: Initialize with null-forgiving operator
    public virtual user? UpdatedByNavigation { get; set; }
}
