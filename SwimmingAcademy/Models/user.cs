using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public class user // Fix for IDE1006: Renamed class to follow PascalCase naming convention.
{
    public int UserId { get; set; } // Fix for IDE1006: Renamed property to follow PascalCase naming convention.
    public string FullName { get; set; } = string.Empty; // Fix for CS8618: Added default value to ensure non-nullable property is initialized.
    public short Site { get; set; }
    public bool Disabled { get; set; } // Fix for IDE1006: Renamed property to follow PascalCase naming convention.
    public short UserTypeID { get; set; }
    public short CreatedBy { get; set; }
    public string Password { get; set; } = string.Empty; // Fix for CS8618: Added default value to ensure non-nullable property is initialized.
    public virtual ICollection<Coach> Coaches { get; set; } = new List<Coach>(); // Fix for CS8618: Added default value to ensure non-nullable property is initialized.
    public virtual AppCode CreatedByNavigation { get; set; } = new AppCode(); // Fix for CS8618: Added default value to ensure non-nullable property is initialized.
    public virtual ICollection<Info2> Info2CreatedByNavigations { get; set; } = new List<Info2>();
    public virtual ICollection<Info2> Info2UpdatedByNavigations { get; set; } = new List<Info2>();
    public virtual ICollection<Info> InfoCreatedByNavigations { get; set; } = new List<Info>();
    public virtual ICollection<Info> InfoUpdatedByNavigations { get; set; } = new List<Info>();
    public virtual ICollection<Invoice_Item> Invoice_ItemCreatedByNavigations { get; set; } = new List<Invoice_Item>();
    public virtual ICollection<Invoice_Item> Invoice_ItemUpdateByNavigations { get; set; } = new List<Invoice_Item>();
    public virtual AppCode SiteNavigation { get; set; } = new AppCode();
    public virtual ICollection<Technical> TechnicalAddedByNavigations { get; set; } = new List<Technical>();
    public virtual ICollection<Technical> TechnicalUpdatedByNavigations { get; set; } = new List<Technical>();
    public virtual ICollection<Time> Times { get; set; } = new List<Time>();
    public virtual AppCode UserType { get; set; } = new AppCode();
    public virtual ICollection<Users_Site> Users_Sites { get; set; } = new List<Users_Site>();
    public virtual ICollection<info1> Info1CreatedByNavigations { get; set; } = new List<info1>();
    public virtual ICollection<info1> Info1UpdatedByNavigations { get; set; } = new List<info1>();
    public virtual ICollection<log1> Log1s { get; set; } = new List<log1>();
    public virtual ICollection<log2> Log2s { get; set; } = new List<log2>();
    public virtual ICollection<log> Logs { get; set; } = new List<log>();
    public virtual ICollection<tran> Trans { get; set; } = new List<tran>();
}
