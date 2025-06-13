using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public class user
{
    public int userid { get; set; }
    public string fullname { get; set; } // Fix for 'Fullname' error: Ensure property exists and matches casing.  
    public short Site { get; set; }
    public bool disabled { get; set; } // Fix for 'Disabled' error: Ensure property exists and matches casing.  
    public short UserTypeID { get; set; }
    public short CreatedBy { get; set; }
    public string Password { get; set; }
    public virtual ICollection<Coach> Coaches { get; set; }
    public virtual AppCode CreatedByNavigation { get; set; }
    public virtual ICollection<Info2> Info2CreatedByNavigations { get; set; }
    public virtual ICollection<Info2> Info2UpdatedByNavigations { get; set; }
    public virtual ICollection<Info> InfocreatedByNavigations { get; set; }
    public virtual ICollection<Info> InfoupdatedByNavigations { get; set; }
    public virtual ICollection<Invoice_Item> Invoice_ItemCreatedByNavigations { get; set; }
    public virtual ICollection<Invoice_Item> Invoice_ItemUpdateByNavigations { get; set; }
    public virtual AppCode SiteNavigation { get; set; }
    public virtual ICollection<Technical> TechnicalAddedbyNavigations { get; set; }
    public virtual ICollection<Technical> TechnicalUpdatedByNavigations { get; set; }
    public virtual ICollection<Time> Times { get; set; }
    public virtual AppCode UserType { get; set; }
    public virtual ICollection<Users_Site> Users_Sites { get; set; }
    public virtual ICollection<info1> info1createdByNavigations { get; set; }
    public virtual ICollection<info1> info1updatedByNavigations { get; set; }
    public virtual ICollection<log1> log1s { get; set; }
    public virtual ICollection<log2> log2s { get; set; }
    public virtual ICollection<log> logs { get; set; }
    public virtual ICollection<tran> trans { get; set; }
}
