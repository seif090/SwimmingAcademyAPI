using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public partial class Invoice_Item
{
    public int ItemID { get; set; }

    public string ItemName { get; set; } = null!;

    public short? Site { get; set; }

    public short? ProductID { get; set; }

    public short? DurationInMonths { get; set; }

    public int ActionID { get; set; }

    public short CreatedAtSite { get; set; }

    public int CreatedBy { get; set; }

    public DateOnly CreatedAtDate { get; set; }

    public int? UpdateBy { get; set; }

    public short? UpdatedAtSite { get; set; }

    public DateOnly? UpdatedAtDate { get; set; }

    public decimal Amount { get; set; }

    public virtual Action Action { get; set; } = null!;

    public virtual AppCode CreatedAtSiteNavigation { get; set; } = null!;

    public virtual user CreatedByNavigation { get; set; } = null!;

    public virtual AppCode? Product { get; set; }

    public virtual AppCode? SiteNavigation { get; set; }

    public virtual user? UpdateByNavigation { get; set; }

    public virtual AppCode? UpdatedAtSiteNavigation { get; set; }

    public virtual ICollection<tran> trans { get; set; } = new List<tran>();
}
