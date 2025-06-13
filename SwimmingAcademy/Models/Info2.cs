using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public partial class Info2
{
    public long SwimmerID { get; set; }

    public string FulllName { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public short StartLevel { get; set; }

    public short CurrentLevel { get; set; }

    public short Site { get; set; }

    public short Gender { get; set; }

    public short CLub { get; set; }

    public short CreatedAtSite { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime createdAtDate { get; set; }

    public short? UpdatedAtSite { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAtDate { get; set; }

    public virtual AppCode CLubNavigation { get; set; } = null!;

    public virtual AppCode CreatedAtSiteNavigation { get; set; } = null!;

    public virtual user? CreatedByNavigation { get; set; }

    public virtual AppCode CurrentLevelNavigation { get; set; } = null!;

    public virtual ICollection<Detail1> Detail1s { get; set; } = new List<Detail1>();

    public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();

    public virtual AppCode GenderNavigation { get; set; } = null!;

    public virtual Parent? Parent { get; set; }

    public virtual AppCode SiteNavigation { get; set; } = null!;

    public virtual AppCode StartLevelNavigation { get; set; } = null!;

    public virtual Technical? Technical { get; set; }

    public virtual ICollection<Time> Times { get; set; } = new List<Time>();

    public virtual AppCode? UpdatedAtSiteNavigation { get; set; }

    public virtual user? UpdatedByNavigation { get; set; }

    public virtual ICollection<log1> log1s { get; set; } = new List<log1>();

    public virtual ICollection<log2> log2s { get; set; } = new List<log2>();

    public virtual ICollection<log> logs { get; set; } = new List<log>();

    public virtual ICollection<tran> trans { get; set; } = new List<tran>();
}
