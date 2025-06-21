using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public class user
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public short Site { get; set; }
    public bool Disabled { get; set; }
    public short UserTypeID { get; set; }
    public short CreatedBy { get; set; }
    public string Password { get; set; }
    public List<string> Modules { get; set; } // Added this property to fix the error

    public virtual ICollection<Coach> Coaches { get; set; }
    public virtual AppCode CreatedByNavigation { get; set; }
    public virtual ICollection<Info2> Info2CreatedByNavigations { get; set; }
    public virtual ICollection<Info2> Info2UpdatedByNavigations { get; set; }
    public virtual ICollection<Info> InfoCreatedByNavigations { get; set; }
    public virtual ICollection<Info> InfoUpdatedByNavigations { get; set; }
    public virtual ICollection<Invoice_Item> Invoice_ItemCreatedByNavigations { get; set; }
    public virtual ICollection<Invoice_Item> Invoice_ItemUpdateByNavigations { get; set; }
    public virtual AppCode SiteNavigation { get; set; }
    public virtual ICollection<Technical> TechnicalAddedByNavigations { get; set; }
    public virtual ICollection<Technical> TechnicalUpdatedByNavigations { get; set; }
    public virtual ICollection<Time> Times { get; set; }
    public virtual AppCode UserType { get; set; }
    public virtual ICollection<Users_Site> Users_Sites { get; set; }
    public virtual ICollection<info1> Info1CreatedByNavigations { get; set; }
    public virtual ICollection<info1> Info1UpdatedByNavigations { get; set; }
    public virtual ICollection<log1> Log1s { get; set; }
    public virtual ICollection<log2> Log2s { get; set; }
    public virtual ICollection<log> Logs { get; set; }
    public virtual ICollection<tran> Trans { get; set; }
}
