using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public class Info
{
    public long PTeamID { get; set; }
    public short PTeamLevel { get; set; }
    public int CoachID { get; set; }
    public string FirstDay { get; set; }
    public string SecondDay { get; set; }
    public string ThirdDay { get; set; }
    public decimal StartTime { get; set; }
    public decimal EndTime { get; set; }
    public short site { get; set; }
    public short createdAtSite { get; set; }
    public int createdBy { get; set; }
    public DateTime createdAtDate { get; set; }
    public short? updatedAtSite { get; set; }
    public int? updatedBy { get; set; }
    public DateTime? updatedAtDate { get; set; }
    public bool ISEnded { get; set; } // Added property to fix CS1061 error  
    public virtual Coach Coach { get; set; }
    public virtual ICollection<Detail> Details { get; set; }
    public virtual AppCode PTeamLevelNavigation { get; set; }
    public virtual AppCode createdAtSiteNavigation { get; set; }
    public virtual user createdByNavigation { get; set; }
    public virtual ICollection<log> logs { get; set; }
    public virtual AppCode siteNavigation { get; set; }
    public virtual AppCode? updatedAtSiteNavigation { get; set; }
    public virtual user? updatedByNavigation { get; set; }
}
