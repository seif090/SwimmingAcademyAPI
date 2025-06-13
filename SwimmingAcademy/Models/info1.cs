using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public partial class info1
{
    public long SchoolID { get; set; }

    public short schoolLevel { get; set; }

    public int CoachID { get; set; }

    public string FirstDay { get; set; } = null!;

    public string SecondDay { get; set; } = null!;

    public decimal StartTime { get; set; }

    public decimal EndTime { get; set; }

    public short SchoolType { get; set; }

    public short site { get; set; }

    public string? NumberOfSwimmers { get; set; }

    public string MaxNumber { get; set; } = null!;

    public short createdAtSite { get; set; }

    public int createdBy { get; set; }

    public DateTime createdAtDate { get; set; }

    public short? updatedAtSite { get; set; }

    public int? updatedBy { get; set; }

    public DateTime? updatedAtDate { get; set; }

    public virtual Coach Coach { get; set; } = null!;

    public virtual ICollection<Detail1> Detail1s { get; set; } = new List<Detail1>();

    public virtual AppCode SchoolTypeNavigation { get; set; } = null!;

    public virtual AppCode createdAtSiteNavigation { get; set; } = null!;

    public virtual user createdByNavigation { get; set; } = null!;

    public virtual ICollection<log1> log1s { get; set; } = new List<log1>();

    public virtual AppCode schoolLevelNavigation { get; set; } = null!;

    public virtual AppCode siteNavigation { get; set; } = null!;

    public virtual AppCode? updatedAtSiteNavigation { get; set; }

    public virtual user? updatedByNavigation { get; set; }
}
