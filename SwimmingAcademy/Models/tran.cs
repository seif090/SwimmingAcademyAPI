using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public partial class tran
{
    public long TID { get; set; }

    public long swimmerID { get; set; }

    public int action { get; set; }

    public short site { get; set; }

    public int itemID { get; set; }

    public short productID { get; set; }

    public short? DurationInMonth { get; set; }

    public decimal TotalAmount { get; set; }

    public int? createdBy { get; set; }

    public DateTime CreateAt { get; set; }

    public virtual Action actionNavigation { get; set; } = null!;

    public virtual user? createdByNavigation { get; set; }

    public virtual Invoice_Item item { get; set; } = null!;

    public virtual AppCode product { get; set; } = null!;

    public virtual AppCode siteNavigation { get; set; } = null!;

    public virtual Info2 swimmer { get; set; } = null!;
}
