using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public partial class Users_Site
{
    public long ID { get; set; }

    public int UserID { get; set; }

    public short Site { get; set; }

    public virtual AppCode SiteNavigation { get; set; } = null!;

    public virtual user User { get; set; } = null!;
}
