using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public partial class Users_Priv
{
    public int ID { get; set; }

    public int ActionID { get; set; }

    public short UserTypeID { get; set; }

    public virtual Action Action { get; set; } = null!;

    public virtual AppCode UserType { get; set; } = null!;
}
