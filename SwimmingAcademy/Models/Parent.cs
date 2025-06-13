using System;
using System.Collections.Generic;

namespace SwimmingAcademy.Models;

public partial class Parent
{
    public long SwimmerID { get; set; }

    public string SwimmerName { get; set; } = null!;

    public string PrimaryPhone { get; set; } = null!;

    public string? SecondaryPhone { get; set; }

    public string PrimaryJop { get; set; } = null!;

    public string? SecondaryJop { get; set; }

    public string Email { get; set; } = null!;

    public short? MemberOf { get; set; }

    public decimal? DiscountRate { get; set; }

    public string Address { get; set; } = null!;

    public virtual AppCode? MemberOfNavigation { get; set; }

    public virtual Info2 Swimmer { get; set; } = null!;
}
