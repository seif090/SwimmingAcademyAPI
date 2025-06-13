using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public partial class Coach
{
    public int CoachID { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public short Gender { get; set; }

    public bool certificated { get; set; }

    public short CoachType { get; set; }

    public short site { get; set; }

    public short createdAtSite { get; set; }

    public int createdBy { get; set; }

    public DateTime createdAtDate { get; set; }

    public DateOnly? updatedAtDate { get; set; }

    public short? updatedAtSite { get; set; }

    public short? updatedBy { get; set; }

    public string mobileNumber { get; set; } = null!;

    public virtual AppCode CoachTypeNavigation { get; set; } = null!;

    public virtual ICollection<Detail1> Detail1s { get; set; } = new List<Detail1>();

    public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();

    public virtual AppCode GenderNavigation { get; set; } = null!;

    public virtual ICollection<Info> Infos { get; set; } = new List<Info>();

    public virtual AppCode createdAtSiteNavigation { get; set; } = null!;

    public virtual user createdByNavigation { get; set; } = null!;

    public virtual ICollection<info1> info1s { get; set; } = new List<info1>();

    public virtual AppCode siteNavigation { get; set; } = null!;

    public virtual AppCode? updatedAtSiteNavigation { get; set; }

    public virtual AppCode? updatedByNavigation { get; set; }
}
