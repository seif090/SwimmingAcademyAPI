using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SwimmingAcademy.Models;
using Action = SwimmingAcademy.Models.Action;

namespace SwimmingAcademy.Data;

public partial class SwimmingAcademyContext : DbContext
{
    public SwimmingAcademyContext()
    {
    }

    public SwimmingAcademyContext(DbContextOptions<SwimmingAcademyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Models.Action> Actions { get; set; }

    public virtual DbSet<AppCode> AppCodes { get; set; }

    public virtual DbSet<Coach> Coaches { get; set; }

    public virtual DbSet<Detail> Details { get; set; }

    public virtual DbSet<Detail1> Details1 { get; set; }

    public virtual DbSet<Ended> Endeds { get; set; }

    public virtual DbSet<Ended1> Endeds1 { get; set; }

    public virtual DbSet<Info> Infos { get; set; }

    public virtual DbSet<Info2> Infos1 { get; set; }

    public virtual DbSet<Invoice_Item> Invoice_Items { get; set; }

    public virtual DbSet<Parent> Parents { get; set; }

    public virtual DbSet<Technical> Technicals { get; set; }

    public virtual DbSet<Time> Times { get; set; }

    public virtual DbSet<Users_Priv> Users_Privs { get; set; }

    public virtual DbSet<Users_Site> Users_Sites { get; set; }

    public virtual DbSet<info1> infos { get; set; }

    public virtual DbSet<log> logs { get; set; }

    public virtual DbSet<log1> logs1 { get; set; }

    public virtual DbSet<log2> logs2 { get; set; }

    public virtual DbSet<tran> trans { get; set; }

    public virtual DbSet<user> users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=SwimminAcadmy;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Action>(entity =>
        {
            entity.HasKey(e => e.ActionID).HasName("PK__Actions__FFE3F4B9FF23B4F3");

            entity.Property(e => e.ActionName).HasMaxLength(100);
            entity.Property(e => e.Module)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AppCode>(entity =>
        {
            entity.HasKey(e => e.sub_id).HasName("PK__AppCodes__694106B082539A84");

            entity.Property(e => e.description).HasMaxLength(50);
        });

        modelBuilder.Entity<Coach>(entity =>
        {
            entity.HasKey(e => e.CoachID).HasName("PK__Coaches__F411D9A18D02D016");

            entity.Property(e => e.FullName).HasMaxLength(120);
            entity.Property(e => e.createdAtDate).HasColumnType("datetime");
            entity.Property(e => e.mobileNumber)
                .HasMaxLength(11)
                .IsUnicode(false);

            entity.HasOne(d => d.CoachTypeNavigation).WithMany(p => p.CoachCoachTypeNavigations)
                .HasForeignKey(d => d.CoachType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Coaches__CoachTy__3C69FB99");

            entity.HasOne(d => d.GenderNavigation).WithMany(p => p.CoachGenderNavigations)
                .HasForeignKey(d => d.Gender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Coaches__Gender__3F466844");

            entity.HasOne(d => d.createdAtSiteNavigation).WithMany(p => p.CoachcreatedAtSiteNavigations)
                .HasForeignKey(d => d.createdAtSite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Coaches__created__3D5E1FD2");

            entity.HasOne(d => d.createdByNavigation).WithMany(p => p.Coaches)
                .HasForeignKey(d => d.createdBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Coaches__created__3E52440B");

            entity.HasOne(d => d.siteNavigation).WithMany(p => p.CoachsiteNavigations)
                .HasForeignKey(d => d.site)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Coaches__site__403A8C7D");

            entity.HasOne(d => d.updatedAtSiteNavigation).WithMany(p => p.CoachupdatedAtSiteNavigations)
                .HasForeignKey(d => d.updatedAtSite)
                .HasConstraintName("FK__Coaches__updated__467D75B8");

            entity.HasOne(d => d.updatedByNavigation).WithMany(p => p.CoachupdatedByNavigations)
                .HasForeignKey(d => d.updatedBy)
                .HasConstraintName("FK__Coaches__updated__477199F1");
        });

        modelBuilder.Entity<Detail>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Details__3214EC2716204D81");

            entity.ToTable("Details", "PreTeam");

            entity.Property(e => e.Attendence).HasMaxLength(10);

            entity.HasOne(d => d.Coach).WithMany(p => p.Details)
                .HasForeignKey(d => d.CoachID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Details__CoachID__43D61337");

            entity.HasOne(d => d.PTeam).WithMany(p => p.Details)
                .HasForeignKey(d => d.PTeamID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Details__PTeamID__41EDCAC5");

            entity.HasOne(d => d.Swimmer).WithMany(p => p.Details)
                .HasForeignKey(d => d.SwimmerID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Details__Swimmer__42E1EEFE");

            entity.HasOne(d => d.SwimmerLevelNavigation).WithMany(p => p.DetailSwimmerLevelNavigations)
                .HasForeignKey(d => d.SwimmerLevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Details__Swimmer__44CA3770");

            entity.HasOne(d => d.siteNavigation).WithMany(p => p.DetailsiteNavigations)
                .HasForeignKey(d => d.site)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Details__site__45BE5BA9");
        });

        modelBuilder.Entity<Detail1>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Details__3214EC270D648956");

            entity.ToTable("Details", "Schools");

            entity.Property(e => e.Attendence).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Coach).WithMany(p => p.Detail1s)
                .HasForeignKey(d => d.CoachID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Details__CoachID__2DE6D218");

            entity.HasOne(d => d.School).WithMany(p => p.Detail1s)
                .HasForeignKey(d => d.SchoolID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Details__SchoolI__2BFE89A6");

            entity.HasOne(d => d.Swimmer).WithMany(p => p.Detail1s)
                .HasForeignKey(d => d.SwimmerID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Details__Swimmer__2CF2ADDF");

            entity.HasOne(d => d.SwimmerLevelNavigation).WithMany(p => p.Detail1SwimmerLevelNavigations)
                .HasForeignKey(d => d.SwimmerLevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Details__Swimmer__2EDAF651");

            entity.HasOne(d => d.siteNavigation).WithMany(p => p.Detail1siteNavigations)
                .HasForeignKey(d => d.site)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Details__site__2FCF1A8A");
        });

        modelBuilder.Entity<Ended>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Ended", "PreTeam");

            entity.HasOne(d => d.EndedByNavigation).WithMany()
                .HasForeignKey(d => d.EndedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ended__EndedBy__5D60DB10");

            entity.HasOne(d => d.PTeam).WithMany()
                .HasForeignKey(d => d.PTeamID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ended__PTeamID__5E54FF49");
        });

        modelBuilder.Entity<Ended1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Ended", "Schools");

            entity.HasOne(d => d.EndedByNavigation).WithMany()
                .HasForeignKey(d => d.EndedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ended__EndedBy__6CA31EA0");

            entity.HasOne(d => d.School).WithMany()
                .HasForeignKey(d => d.Schoolid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ended__Schoolid__6D9742D9");
        });

        modelBuilder.Entity<Info>(entity =>
        {
            entity.HasKey(e => e.PTeamID).HasName("PK__Info__C79A741AF2980D16");

            entity.ToTable("Info", "PreTeam");

            entity.Property(e => e.EndTime)
                .HasDefaultValueSql("('09:30:00')")
                .HasColumnType("decimal(18, 0)");
            entity.Property(e => e.FirstDay).HasMaxLength(10);
            entity.Property(e => e.SecondDay).HasMaxLength(10);
            entity.Property(e => e.StartTime)
                .HasDefaultValueSql("('08:00:00')")
                .HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ThirdDay).HasMaxLength(10);
            entity.Property(e => e.createdAtDate).HasColumnType("datetime");
            entity.Property(e => e.updatedAtDate).HasColumnType("datetime");

            entity.HasOne(d => d.Coach).WithMany(p => p.Infos)
                .HasForeignKey(d => d.CoachID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Info__CoachID__3493CFA7");

            entity.HasOne(d => d.PTeamLevelNavigation).WithMany(p => p.InfoPTeamLevelNavigations)
                .HasForeignKey(d => d.PTeamLevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Info__PTeamLevel__339FAB6E");

            entity.HasOne(d => d.createdAtSiteNavigation).WithMany(p => p.InfocreatedAtSiteNavigations)
                .HasForeignKey(d => d.createdAtSite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Info__createdAtS__367C1819");

            entity.HasOne(d => d.createdByNavigation).WithMany(p => p.InfocreatedByNavigations)
                .HasForeignKey(d => d.createdBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Info__createdBy__37703C52");

            entity.HasOne(d => d.siteNavigation).WithMany(p => p.InfositeNavigations)
                .HasForeignKey(d => d.site)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Info__site__3587F3E0");

            entity.HasOne(d => d.updatedAtSiteNavigation).WithMany(p => p.InfoupdatedAtSiteNavigations)
                .HasForeignKey(d => d.updatedAtSite)
                .HasConstraintName("FK__Info__updatedAtS__3864608B");

            entity.HasOne(d => d.updatedByNavigation).WithMany(p => p.InfoupdatedByNavigations)
                .HasForeignKey(d => d.updatedBy)
                .HasConstraintName("FK__Info__updatedBy__395884C4");
        });

        modelBuilder.Entity<Info2>(entity =>
        {
            entity.HasKey(e => e.SwimmerID).HasName("PK__Info__E8BD7A652CB82250");

            entity.ToTable("Info", "Swimmers");

            entity.Property(e => e.FulllName).HasMaxLength(120);
            entity.Property(e => e.UpdatedAtDate).HasColumnType("datetime");
            entity.Property(e => e.createdAtDate).HasColumnType("datetime");

            entity.HasOne(d => d.CLubNavigation).WithMany(p => p.Info2CLubNavigations)
                .HasForeignKey(d => d.CLub)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Info__CLub__7AF13DF7");

            entity.HasOne(d => d.CreatedAtSiteNavigation).WithMany(p => p.Info2CreatedAtSiteNavigations)
                .HasForeignKey(d => d.CreatedAtSite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Info__CreatedAtS__7BE56230");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Info2CreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Info__CreatedBy__7CD98669");

            entity.HasOne(d => d.CurrentLevelNavigation).WithMany(p => p.Info2CurrentLevelNavigations)
                .HasForeignKey(d => d.CurrentLevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Info__CurrentLev__7A672E12");

            entity.HasOne(d => d.GenderNavigation).WithMany(p => p.Info2GenderNavigations)
                .HasForeignKey(d => d.Gender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Info__Gender__7C4F7684");

            entity.HasOne(d => d.SiteNavigation).WithMany(p => p.Info2SiteNavigations)
                .HasForeignKey(d => d.Site)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Info__Site__7B5B524B");

            entity.HasOne(d => d.StartLevelNavigation).WithMany(p => p.Info2StartLevelNavigations)
                .HasForeignKey(d => d.StartLevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Info__StartLevel__00AA174D");

            entity.HasOne(d => d.UpdatedAtSiteNavigation).WithMany(p => p.Info2UpdatedAtSiteNavigations)
                .HasForeignKey(d => d.UpdatedAtSite)
                .HasConstraintName("FK__Info__UpdatedAtS__00200768");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.Info2UpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK__Info__UpdatedBy__01142BA1");
        });

        modelBuilder.Entity<Invoice_Item>(entity =>
        {
            entity.HasKey(e => e.ItemID).HasName("PK__Invoice___727E83EB2E050C43");

            entity.ToTable("Invoice_Item");

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.ItemName)
                .HasMaxLength(120)
                .IsUnicode(false);

            entity.HasOne(d => d.Action).WithMany(p => p.Invoice_Items)
                .HasForeignKey(d => d.ActionID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice_I__Actio__02C769E9");

            entity.HasOne(d => d.CreatedAtSiteNavigation).WithMany(p => p.Invoice_ItemCreatedAtSiteNavigations)
                .HasForeignKey(d => d.CreatedAtSite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice_I__Creat__03BB8E22");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Invoice_ItemCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice_I__Creat__04AFB25B");

            entity.HasOne(d => d.Product).WithMany(p => p.Invoice_ItemProducts)
                .HasForeignKey(d => d.ProductID)
                .HasConstraintName("FK__Invoice_I__Produ__01D345B0");

            entity.HasOne(d => d.SiteNavigation).WithMany(p => p.Invoice_ItemSiteNavigations)
                .HasForeignKey(d => d.Site)
                .HasConstraintName("FK__Invoice_It__Site__00DF2177");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.Invoice_ItemUpdateByNavigations)
                .HasForeignKey(d => d.UpdateBy)
                .HasConstraintName("FK__Invoice_I__Updat__05A3D694");

            entity.HasOne(d => d.UpdatedAtSiteNavigation).WithMany(p => p.Invoice_ItemUpdatedAtSiteNavigations)
                .HasForeignKey(d => d.UpdatedAtSite)
                .HasConstraintName("FK__Invoice_I__Updat__0697FACD");
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasKey(e => e.SwimmerID).HasName("PK__Parent__E8BD7A65A62F86BB");

            entity.ToTable("Parent", "Swimmers");

            entity.Property(e => e.SwimmerID).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(120);
            entity.Property(e => e.DiscountRate).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PrimaryJop).HasMaxLength(100);
            entity.Property(e => e.PrimaryPhone)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.SecondaryJop).HasMaxLength(100);
            entity.Property(e => e.SecondaryPhone)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.SwimmerName).HasMaxLength(120);

            entity.HasOne(d => d.MemberOfNavigation).WithMany(p => p.Parents)
                .HasForeignKey(d => d.MemberOf)
                .HasConstraintName("FK__Parent__MemberOf__075714DC");

            entity.HasOne(d => d.Swimmer).WithOne(p => p.Parent)
                .HasForeignKey<Parent>(d => d.SwimmerID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Parent__SwimmerI__084B3915");
        });

        modelBuilder.Entity<Technical>(entity =>
        {
            entity.HasKey(e => e.SwimmerID).HasName("PK__Technica__E8BD7A65C169533B");

            entity.ToTable("Technical", "Swimmers");

            entity.Property(e => e.SwimmerID).ValueGeneratedNever();

            entity.HasOne(d => d.AddedbyNavigation).WithMany(p => p.TechnicalAddedbyNavigations)
                .HasForeignKey(d => d.Addedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Technical__Added__093F5D4E");

            entity.HasOne(d => d.CurrentLevelNavigation).WithMany(p => p.TechnicalCurrentLevelNavigations)
                .HasForeignKey(d => d.CurrentLevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Technical__Curre__0A338187");

            entity.HasOne(d => d.FirstSPNavigation).WithMany(p => p.TechnicalFirstSPNavigations)
                .HasForeignKey(d => d.FirstSP)
                .HasConstraintName("FK__Technical__First__0B27A5C0");

            entity.HasOne(d => d.LastStarNavigation).WithMany(p => p.TechnicalLastStarNavigations)
                .HasForeignKey(d => d.LastStar)
                .HasConstraintName("FK__Technical__LastS__0C1BC9F9");

            entity.HasOne(d => d.SecondSPNavigation).WithMany(p => p.TechnicalSecondSPNavigations)
                .HasForeignKey(d => d.SecondSP)
                .HasConstraintName("FK__Technical__Secon__0D0FEE32");

            entity.HasOne(d => d.SiteNavigation).WithMany(p => p.TechnicalSiteNavigations)
                .HasForeignKey(d => d.Site)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Technical__Site__0E04126B");

            entity.HasOne(d => d.Swimmer).WithOne(p => p.Technical)
                .HasForeignKey<Technical>(d => d.SwimmerID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Technical__Swimm__0D7A0286");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.TechnicalUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK__Technical__Updat__0FEC5ADD");
        });

        modelBuilder.Entity<Time>(entity =>
        {
            entity.HasKey(e => e.TID).HasName("PK__Times__C456D7296CF66AEE");

            entity.ToTable("Times", "Swimmers");

            entity.Property(e => e.AddedAt).HasColumnType("datetime");
            entity.Property(e => e.Time1)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Time");

            entity.HasOne(d => d.AddedBYNavigation).WithMany(p => p.Times)
                .HasForeignKey(d => d.AddedBY)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Times__AddedBY__18EBB532");

            entity.HasOne(d => d.Race).WithMany(p => p.TimeRaces)
                .HasForeignKey(d => d.RaceID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Times__RaceID__17036CC0");

            entity.HasOne(d => d.RacePlace).WithMany(p => p.TimeRacePlaces)
                .HasForeignKey(d => d.RacePlaceID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Times__RacePlace__17F790F9");

            entity.HasOne(d => d.SiteNavigation).WithMany(p => p.TimeSiteNavigations)
                .HasForeignKey(d => d.Site)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Times__Site__160F4887");

            entity.HasOne(d => d.Swimmer).WithMany(p => p.Times)
                .HasForeignKey(d => d.SwimmerID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Times__SwimmerID__151B244E");
        });

        modelBuilder.Entity<Users_Priv>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Users_Pr__3214EC270E3A9EF0");

            entity.ToTable("Users_Priv");

            entity.HasOne(d => d.Action).WithMany(p => p.Users_Privs)
                .HasForeignKey(d => d.ActionID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users_Pri__Actio__57A801BA");

            entity.HasOne(d => d.UserType).WithMany(p => p.Users_Privs)
                .HasForeignKey(d => d.UserTypeID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users_Pri__UserT__589C25F3");
        });

        modelBuilder.Entity<Users_Site>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Users_Si__3214EC275E69442C");

            entity.HasOne(d => d.SiteNavigation).WithMany(p => p.Users_Sites)
                .HasForeignKey(d => d.Site)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users_Site__Site__5A846E65");

            entity.HasOne(d => d.User).WithMany(p => p.Users_Sites)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users_Sit__UserI__59904A2C");
        });

        modelBuilder.Entity<info1>(entity =>
        {
            entity.HasKey(e => e.SchoolID).HasName("PK__info__3DA4677BD433C5F5");

            entity.ToTable("info", "Schools");

            entity.Property(e => e.EndTime)
                .HasDefaultValueSql("('09:30:00')")
                .HasColumnType("decimal(18, 0)");
            entity.Property(e => e.FirstDay).HasMaxLength(10);
            entity.Property(e => e.MaxNumber)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.NumberOfSwimmers)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.SecondDay).HasMaxLength(10);
            entity.Property(e => e.StartTime)
                .HasDefaultValueSql("('08:30:00')")
                .HasColumnType("decimal(18, 0)");
            entity.Property(e => e.createdAtDate).HasColumnType("datetime");
            entity.Property(e => e.updatedAtDate).HasColumnType("datetime");

            entity.HasOne(d => d.Coach).WithMany(p => p.info1s)
                .HasForeignKey(d => d.CoachID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__info__CoachID__1CBC4616");

            entity.HasOne(d => d.SchoolTypeNavigation).WithMany(p => p.info1SchoolTypeNavigations)
                .HasForeignKey(d => d.SchoolType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__info__SchoolType__1DB06A4F");

            entity.HasOne(d => d.createdAtSiteNavigation).WithMany(p => p.info1createdAtSiteNavigations)
                .HasForeignKey(d => d.createdAtSite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__info__createdAtS__1F98B2C1");

            entity.HasOne(d => d.createdByNavigation).WithMany(p => p.info1createdByNavigations)
                .HasForeignKey(d => d.createdBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__info__createdBy__208CD6FA");

            entity.HasOne(d => d.schoolLevelNavigation).WithMany(p => p.info1schoolLevelNavigations)
                .HasForeignKey(d => d.schoolLevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__info__schoolLeve__1BC821DD");

            entity.HasOne(d => d.siteNavigation).WithMany(p => p.info1siteNavigations)
                .HasForeignKey(d => d.site)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__info__site__1EA48E88");

            entity.HasOne(d => d.updatedAtSiteNavigation).WithMany(p => p.info1updatedAtSiteNavigations)
                .HasForeignKey(d => d.updatedAtSite)
                .HasConstraintName("FK__info__updatedAtS__2180FB33");

            entity.HasOne(d => d.updatedByNavigation).WithMany(p => p.info1updatedByNavigations)
                .HasForeignKey(d => d.updatedBy)
                .HasConstraintName("FK__info__updatedBy__22751F6C");
        });

        modelBuilder.Entity<log>(entity =>
        {
            entity.HasKey(e => e.LID).HasName("PK__log__C65557219DB6DF1E");

            entity.ToTable("log", "PreTeam");

            entity.HasOne(d => d.Action).WithMany(p => p.logs)
                .HasForeignKey(d => d.ActionID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__log__ActionID__65F62111");

            entity.HasOne(d => d.CreatedAtSiteNavigation).WithMany(p => p.logs)
                .HasForeignKey(d => d.CreatedAtSite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__log__CreatedAtSi__66EA454A");

            entity.HasOne(d => d.Pteam).WithMany(p => p.logs)
                .HasForeignKey(d => d.PteamID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__log__PteamID__68D28DBC");

            entity.HasOne(d => d.Swimmer).WithMany(p => p.logs)
                .HasForeignKey(d => d.SwimmerID)
                .HasConstraintName("FK__log__SwimmerID__69C6B1F5");

            entity.HasOne(d => d.createdbyNavigation).WithMany(p => p.logs)
                .HasForeignKey(d => d.createdby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__log__createdby__67DE6983");
        });

        modelBuilder.Entity<log1>(entity =>
        {
            entity.HasKey(e => e.LID).HasName("PK__log__C6555721D313A0E7");

            entity.ToTable("log", "Schools");

            entity.HasOne(d => d.Action).WithMany(p => p.log1s)
                .HasForeignKey(d => d.ActionID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__log__ActionID__73852659");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.log1s)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__log__CreatedBy__76619304");

            entity.HasOne(d => d.createdAtsiteNavigation).WithMany(p => p.log1s)
                .HasForeignKey(d => d.createdAtsite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__log__createdAtsi__756D6ECB");

            entity.HasOne(d => d.school).WithMany(p => p.log1s)
                .HasForeignKey(d => d.schoolID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__log__schoolID__72910220");

            entity.HasOne(d => d.swimmer).WithMany(p => p.log1s)
                .HasForeignKey(d => d.swimmerID)
                .HasConstraintName("FK__log__swimmerID__74794A92");
        });

        modelBuilder.Entity<log2>(entity =>
        {
            entity.HasKey(e => e.LID).HasName("PK__log__C6555721C4CDAF7A");

            entity.ToTable("log", "Swimmers");

            entity.HasOne(d => d.Action).WithMany(p => p.log2s)
                .HasForeignKey(d => d.ActionID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__log__ActionID__038683F8");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.log2s)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__log__CreatedBy__056ECC6A");

            entity.HasOne(d => d.createdAtsiteNavigation).WithMany(p => p.log2s)
                .HasForeignKey(d => d.createdAtsite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__log__createdAtsi__047AA831");

            entity.HasOne(d => d.swimmer).WithMany(p => p.log2s)
                .HasForeignKey(d => d.swimmerID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__log__swimmerID__0662F0A3");
        });

        modelBuilder.Entity<tran>(entity =>
        {
            entity.HasKey(e => e.TID).HasName("PK__trans__C456D729C2A6D762");

            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasColumnType("money");

            entity.HasOne(d => d.actionNavigation).WithMany(p => p.trans)
                .HasForeignKey(d => d.action)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__trans__action__4F12BBB9");

            entity.HasOne(d => d.createdByNavigation).WithMany(p => p.trans)
                .HasForeignKey(d => d.createdBy)
                .HasConstraintName("FK__trans__createdBy__5006DFF2");

            entity.HasOne(d => d.item).WithMany(p => p.trans)
                .HasForeignKey(d => d.itemID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__trans__itemID__50FB042B");

            entity.HasOne(d => d.product).WithMany(p => p.tranproducts)
                .HasForeignKey(d => d.productID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__trans__productID__51EF2864");

            entity.HasOne(d => d.siteNavigation).WithMany(p => p.transiteNavigations)
                .HasForeignKey(d => d.site)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__trans__site__52E34C9D");

            entity.HasOne(d => d.swimmer).WithMany(p => p.trans)
                .HasForeignKey(d => d.swimmerID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__trans__swimmerID__53D770D6");
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.userid).HasName("PK__users__CBA1B2575DDDE890");

            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.fullname).HasMaxLength(120);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.userCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__users__CreatedBy__54CB950F");

            entity.HasOne(d => d.SiteNavigation).WithMany(p => p.userSiteNavigations)
                .HasForeignKey(d => d.Site)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__users__Site__398D8EEE");

            entity.HasOne(d => d.UserType).WithMany(p => p.userUserTypes)
                .HasForeignKey(d => d.UserTypeID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__users__UserTypeI__56B3DD81");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
