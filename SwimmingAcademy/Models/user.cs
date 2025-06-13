using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public partial class user
{
    public int userid { get; set; }

    public string fullname { get; set; } = null!;

    public short Site { get; set; }

    public bool disabled { get; set; }

    public short UserTypeID { get; set; }

    public short CreatedBy { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<Coach> Coaches { get; set; } = new List<Coach>();

    public virtual AppCode CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Info2> Info2CreatedByNavigations { get; set; } = new List<Info2>();

    public virtual ICollection<Info2> Info2UpdatedByNavigations { get; set; } = new List<Info2>();

    public virtual ICollection<Info> InfocreatedByNavigations { get; set; } = new List<Info>();

    public virtual ICollection<Info> InfoupdatedByNavigations { get; set; } = new List<Info>();

    public virtual ICollection<Invoice_Item> Invoice_ItemCreatedByNavigations { get; set; } = new List<Invoice_Item>();

    public virtual ICollection<Invoice_Item> Invoice_ItemUpdateByNavigations { get; set; } = new List<Invoice_Item>();

    public virtual AppCode SiteNavigation { get; set; } = null!;

    public virtual ICollection<Technical> TechnicalAddedbyNavigations { get; set; } = new List<Technical>();

    public virtual ICollection<Technical> TechnicalUpdatedByNavigations { get; set; } = new List<Technical>();

    public virtual ICollection<Time> Times { get; set; } = new List<Time>();

    public virtual AppCode UserType { get; set; } = null!;

    public virtual ICollection<Users_Site> Users_Sites { get; set; } = new List<Users_Site>();

    public virtual ICollection<info1> info1createdByNavigations { get; set; } = new List<info1>();

    public virtual ICollection<info1> info1updatedByNavigations { get; set; } = new List<info1>();

    public virtual ICollection<log1> log1s { get; set; } = new List<log1>();

    public virtual ICollection<log2> log2s { get; set; } = new List<log2>();

    public virtual ICollection<log> logs { get; set; } = new List<log>();

    public virtual ICollection<tran> trans { get; set; } = new List<tran>();
}
