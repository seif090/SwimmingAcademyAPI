using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public partial class Action
{
    public int ActionID { get; set; }

    public string ActionName { get; set; } = null!;

    public bool? SameSite { get; set; }

    public bool Disabled { get; set; }

    public string Module { get; set; } = null!;

    public bool IsInv { get; set; }

    public virtual ICollection<Invoice_Item> Invoice_Items { get; set; } = new List<Invoice_Item>();

    public virtual ICollection<Users_Priv> Users_Privs { get; set; } = new List<Users_Priv>();

    public virtual ICollection<log1> log1s { get; set; } = new List<log1>();

    public virtual ICollection<log2> log2s { get; set; } = new List<log2>();

    public virtual ICollection<log> logs { get; set; } = new List<log>();

    public virtual ICollection<tran> trans { get; set; } = new List<tran>();
}
