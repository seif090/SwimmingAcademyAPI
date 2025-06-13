using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public partial class AppCode
{
    public short main_id { get; set; }

    public short sub_id { get; set; }

    public string description { get; set; } = null!;

    public bool disabled { get; set; }

    public virtual ICollection<Coach> CoachCoachTypeNavigations { get; set; } = new List<Coach>();

    public virtual ICollection<Coach> CoachGenderNavigations { get; set; } = new List<Coach>();

    public virtual ICollection<Coach> CoachcreatedAtSiteNavigations { get; set; } = new List<Coach>();

    public virtual ICollection<Coach> CoachsiteNavigations { get; set; } = new List<Coach>();

    public virtual ICollection<Coach> CoachupdatedAtSiteNavigations { get; set; } = new List<Coach>();

    public virtual ICollection<Coach> CoachupdatedByNavigations { get; set; } = new List<Coach>();

    public virtual ICollection<Detail1> Detail1SwimmerLevelNavigations { get; set; } = new List<Detail1>();

    public virtual ICollection<Detail1> Detail1siteNavigations { get; set; } = new List<Detail1>();

    public virtual ICollection<Detail> DetailSwimmerLevelNavigations { get; set; } = new List<Detail>();

    public virtual ICollection<Detail> DetailsiteNavigations { get; set; } = new List<Detail>();

    public virtual ICollection<Info2> Info2CLubNavigations { get; set; } = new List<Info2>();

    public virtual ICollection<Info2> Info2CreatedAtSiteNavigations { get; set; } = new List<Info2>();

    public virtual ICollection<Info2> Info2CurrentLevelNavigations { get; set; } = new List<Info2>();

    public virtual ICollection<Info2> Info2GenderNavigations { get; set; } = new List<Info2>();

    public virtual ICollection<Info2> Info2SiteNavigations { get; set; } = new List<Info2>();

    public virtual ICollection<Info2> Info2StartLevelNavigations { get; set; } = new List<Info2>();

    public virtual ICollection<Info2> Info2UpdatedAtSiteNavigations { get; set; } = new List<Info2>();

    public virtual ICollection<Info> InfoPTeamLevelNavigations { get; set; } = new List<Info>();

    public virtual ICollection<Info> InfocreatedAtSiteNavigations { get; set; } = new List<Info>();

    public virtual ICollection<Info> InfositeNavigations { get; set; } = new List<Info>();

    public virtual ICollection<Info> InfoupdatedAtSiteNavigations { get; set; } = new List<Info>();

    public virtual ICollection<Invoice_Item> Invoice_ItemCreatedAtSiteNavigations { get; set; } = new List<Invoice_Item>();

    public virtual ICollection<Invoice_Item> Invoice_ItemProducts { get; set; } = new List<Invoice_Item>();

    public virtual ICollection<Invoice_Item> Invoice_ItemSiteNavigations { get; set; } = new List<Invoice_Item>();

    public virtual ICollection<Invoice_Item> Invoice_ItemUpdatedAtSiteNavigations { get; set; } = new List<Invoice_Item>();

    public virtual ICollection<Parent> Parents { get; set; } = new List<Parent>();

    public virtual ICollection<Technical> TechnicalCurrentLevelNavigations { get; set; } = new List<Technical>();

    public virtual ICollection<Technical> TechnicalFirstSPNavigations { get; set; } = new List<Technical>();

    public virtual ICollection<Technical> TechnicalLastStarNavigations { get; set; } = new List<Technical>();

    public virtual ICollection<Technical> TechnicalSecondSPNavigations { get; set; } = new List<Technical>();

    public virtual ICollection<Technical> TechnicalSiteNavigations { get; set; } = new List<Technical>();

    public virtual ICollection<Time> TimeRacePlaces { get; set; } = new List<Time>();

    public virtual ICollection<Time> TimeRaces { get; set; } = new List<Time>();

    public virtual ICollection<Time> TimeSiteNavigations { get; set; } = new List<Time>();

    public virtual ICollection<Users_Priv> Users_Privs { get; set; } = new List<Users_Priv>();

    public virtual ICollection<Users_Site> Users_Sites { get; set; } = new List<Users_Site>();

    public virtual ICollection<info1> info1SchoolTypeNavigations { get; set; } = new List<info1>();

    public virtual ICollection<info1> info1createdAtSiteNavigations { get; set; } = new List<info1>();

    public virtual ICollection<info1> info1schoolLevelNavigations { get; set; } = new List<info1>();

    public virtual ICollection<info1> info1siteNavigations { get; set; } = new List<info1>();

    public virtual ICollection<info1> info1updatedAtSiteNavigations { get; set; } = new List<info1>();

    public virtual ICollection<log1> log1s { get; set; } = new List<log1>();

    public virtual ICollection<log2> log2s { get; set; } = new List<log2>();

    public virtual ICollection<log> logs { get; set; } = new List<log>();

    public virtual ICollection<tran> tranproducts { get; set; } = new List<tran>();

    public virtual ICollection<tran> transiteNavigations { get; set; } = new List<tran>();

    public virtual ICollection<user> userCreatedByNavigations { get; set; } = new List<user>();

    public virtual ICollection<user> userSiteNavigations { get; set; } = new List<user>();

    public virtual ICollection<user> userUserTypes { get; set; } = new List<user>();
}
