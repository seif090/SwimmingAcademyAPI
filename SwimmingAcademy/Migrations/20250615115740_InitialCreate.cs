using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwimmingAcademy.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PreTeam");

            migrationBuilder.EnsureSchema(
                name: "Schools");

            migrationBuilder.EnsureSchema(
                name: "Swimmers");

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    ActionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SameSite = table.Column<bool>(type: "bit", nullable: true),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Module = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    IsInv = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Actions__FFE3F4B9FF23B4F3", x => x.ActionID);
                });

            migrationBuilder.CreateTable(
                name: "AppCodes",
                columns: table => new
                {
                    sub_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    main_id = table.Column<short>(type: "smallint", nullable: false),
                    description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    disabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AppCodes__694106B082539A84", x => x.sub_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullname = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Site = table.Column<short>(type: "smallint", nullable: false),
                    disabled = table.Column<bool>(type: "bit", nullable: false),
                    UserTypeID = table.Column<short>(type: "smallint", nullable: false),
                    CreatedBy = table.Column<short>(type: "smallint", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__CBA1B2575DDDE890", x => x.userid);
                    table.ForeignKey(
                        name: "FK__users__CreatedBy__54CB950F",
                        column: x => x.CreatedBy,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__users__Site__398D8EEE",
                        column: x => x.Site,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__users__UserTypeI__56B3DD81",
                        column: x => x.UserTypeID,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                });

            migrationBuilder.CreateTable(
                name: "Users_Priv",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    UserTypeID = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users_Pr__3214EC270E3A9EF0", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Users_Pri__Actio__57A801BA",
                        column: x => x.ActionID,
                        principalTable: "Actions",
                        principalColumn: "ActionID");
                    table.ForeignKey(
                        name: "FK__Users_Pri__UserT__589C25F3",
                        column: x => x.UserTypeID,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    CoachID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<short>(type: "smallint", nullable: false),
                    certificated = table.Column<bool>(type: "bit", nullable: false),
                    CoachType = table.Column<short>(type: "smallint", nullable: false),
                    site = table.Column<short>(type: "smallint", nullable: false),
                    createdAtSite = table.Column<short>(type: "smallint", nullable: false),
                    createdBy = table.Column<int>(type: "int", nullable: false),
                    createdAtDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    updatedAtDate = table.Column<DateOnly>(type: "date", nullable: true),
                    updatedAtSite = table.Column<short>(type: "smallint", nullable: true),
                    updatedBy = table.Column<short>(type: "smallint", nullable: true),
                    mobileNumber = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Coaches__F411D9A18D02D016", x => x.CoachID);
                    table.ForeignKey(
                        name: "FK__Coaches__CoachTy__3C69FB99",
                        column: x => x.CoachType,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Coaches__Gender__3F466844",
                        column: x => x.Gender,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Coaches__created__3D5E1FD2",
                        column: x => x.createdAtSite,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Coaches__created__3E52440B",
                        column: x => x.createdBy,
                        principalTable: "users",
                        principalColumn: "userid");
                    table.ForeignKey(
                        name: "FK__Coaches__site__403A8C7D",
                        column: x => x.site,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Coaches__updated__467D75B8",
                        column: x => x.updatedAtSite,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Coaches__updated__477199F1",
                        column: x => x.updatedBy,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                });

            migrationBuilder.CreateTable(
                name: "Info",
                schema: "Swimmers",
                columns: table => new
                {
                    SwimmerID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FulllName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartLevel = table.Column<short>(type: "smallint", nullable: false),
                    CurrentLevel = table.Column<short>(type: "smallint", nullable: false),
                    Site = table.Column<short>(type: "smallint", nullable: false),
                    Gender = table.Column<short>(type: "smallint", nullable: false),
                    CLub = table.Column<short>(type: "smallint", nullable: false),
                    CreatedAtSite = table.Column<short>(type: "smallint", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    createdAtDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAtSite = table.Column<short>(type: "smallint", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAtDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Info__E8BD7A652CB82250", x => x.SwimmerID);
                    table.ForeignKey(
                        name: "FK__Info__CLub__7AF13DF7",
                        column: x => x.CLub,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Info__CreatedAtS__7BE56230",
                        column: x => x.CreatedAtSite,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Info__CreatedBy__7CD98669",
                        column: x => x.CreatedBy,
                        principalTable: "users",
                        principalColumn: "userid");
                    table.ForeignKey(
                        name: "FK__Info__CurrentLev__7A672E12",
                        column: x => x.CurrentLevel,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Info__Gender__7C4F7684",
                        column: x => x.Gender,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Info__Site__7B5B524B",
                        column: x => x.Site,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Info__StartLevel__00AA174D",
                        column: x => x.StartLevel,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Info__UpdatedAtS__00200768",
                        column: x => x.UpdatedAtSite,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Info__UpdatedBy__01142BA1",
                        column: x => x.UpdatedBy,
                        principalTable: "users",
                        principalColumn: "userid");
                });

            migrationBuilder.CreateTable(
                name: "Invoice_Item",
                columns: table => new
                {
                    ItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: false),
                    Site = table.Column<short>(type: "smallint", nullable: true),
                    ProductID = table.Column<short>(type: "smallint", nullable: true),
                    DurationInMonths = table.Column<short>(type: "smallint", nullable: true),
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    CreatedAtSite = table.Column<short>(type: "smallint", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAtDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UpdateBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAtSite = table.Column<short>(type: "smallint", nullable: true),
                    UpdatedAtDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Invoice___727E83EB2E050C43", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK__Invoice_I__Actio__02C769E9",
                        column: x => x.ActionID,
                        principalTable: "Actions",
                        principalColumn: "ActionID");
                    table.ForeignKey(
                        name: "FK__Invoice_I__Creat__03BB8E22",
                        column: x => x.CreatedAtSite,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Invoice_I__Creat__04AFB25B",
                        column: x => x.CreatedBy,
                        principalTable: "users",
                        principalColumn: "userid");
                    table.ForeignKey(
                        name: "FK__Invoice_I__Produ__01D345B0",
                        column: x => x.ProductID,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Invoice_I__Updat__05A3D694",
                        column: x => x.UpdateBy,
                        principalTable: "users",
                        principalColumn: "userid");
                    table.ForeignKey(
                        name: "FK__Invoice_I__Updat__0697FACD",
                        column: x => x.UpdatedAtSite,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Invoice_It__Site__00DF2177",
                        column: x => x.Site,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                });

            migrationBuilder.CreateTable(
                name: "Users_Sites",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Site = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users_Si__3214EC275E69442C", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Users_Sit__UserI__59904A2C",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "userid");
                    table.ForeignKey(
                        name: "FK__Users_Site__Site__5A846E65",
                        column: x => x.Site,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                });

            migrationBuilder.CreateTable(
                name: "info",
                schema: "Schools",
                columns: table => new
                {
                    SchoolID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schoolLevel = table.Column<short>(type: "smallint", nullable: false),
                    CoachID = table.Column<int>(type: "int", nullable: false),
                    FirstDay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SecondDay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StartTime = table.Column<decimal>(type: "decimal(18,0)", nullable: false, defaultValueSql: "('08:30:00')"),
                    EndTime = table.Column<decimal>(type: "decimal(18,0)", nullable: false, defaultValueSql: "('09:30:00')"),
                    SchoolType = table.Column<short>(type: "smallint", nullable: false),
                    site = table.Column<short>(type: "smallint", nullable: false),
                    NumberOfSwimmers = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    MaxNumber = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    createdAtSite = table.Column<short>(type: "smallint", nullable: false),
                    createdBy = table.Column<int>(type: "int", nullable: false),
                    createdAtDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    updatedAtSite = table.Column<short>(type: "smallint", nullable: true),
                    updatedBy = table.Column<int>(type: "int", nullable: true),
                    updatedAtDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__info__3DA4677BD433C5F5", x => x.SchoolID);
                    table.ForeignKey(
                        name: "FK__info__CoachID__1CBC4616",
                        column: x => x.CoachID,
                        principalTable: "Coaches",
                        principalColumn: "CoachID");
                    table.ForeignKey(
                        name: "FK__info__SchoolType__1DB06A4F",
                        column: x => x.SchoolType,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__info__createdAtS__1F98B2C1",
                        column: x => x.createdAtSite,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__info__createdBy__208CD6FA",
                        column: x => x.createdBy,
                        principalTable: "users",
                        principalColumn: "userid");
                    table.ForeignKey(
                        name: "FK__info__schoolLeve__1BC821DD",
                        column: x => x.schoolLevel,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__info__site__1EA48E88",
                        column: x => x.site,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__info__updatedAtS__2180FB33",
                        column: x => x.updatedAtSite,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__info__updatedBy__22751F6C",
                        column: x => x.updatedBy,
                        principalTable: "users",
                        principalColumn: "userid");
                });

            migrationBuilder.CreateTable(
                name: "Info",
                schema: "PreTeam",
                columns: table => new
                {
                    PTeamID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PTeamLevel = table.Column<short>(type: "smallint", nullable: false),
                    CoachID = table.Column<int>(type: "int", nullable: false),
                    FirstDay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SecondDay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ThirdDay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StartTime = table.Column<decimal>(type: "decimal(18,0)", nullable: false, defaultValueSql: "('08:00:00')"),
                    EndTime = table.Column<decimal>(type: "decimal(18,0)", nullable: false, defaultValueSql: "('09:30:00')"),
                    site = table.Column<short>(type: "smallint", nullable: false),
                    createdAtSite = table.Column<short>(type: "smallint", nullable: false),
                    createdBy = table.Column<int>(type: "int", nullable: false),
                    createdAtDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    updatedAtSite = table.Column<short>(type: "smallint", nullable: true),
                    updatedBy = table.Column<int>(type: "int", nullable: true),
                    updatedAtDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ISEnded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Info__C79A741AF2980D16", x => x.PTeamID);
                    table.ForeignKey(
                        name: "FK__Info__CoachID__3493CFA7",
                        column: x => x.CoachID,
                        principalTable: "Coaches",
                        principalColumn: "CoachID");
                    table.ForeignKey(
                        name: "FK__Info__PTeamLevel__339FAB6E",
                        column: x => x.PTeamLevel,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Info__createdAtS__367C1819",
                        column: x => x.createdAtSite,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Info__createdBy__37703C52",
                        column: x => x.createdBy,
                        principalTable: "users",
                        principalColumn: "userid");
                    table.ForeignKey(
                        name: "FK__Info__site__3587F3E0",
                        column: x => x.site,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Info__updatedAtS__3864608B",
                        column: x => x.updatedAtSite,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Info__updatedBy__395884C4",
                        column: x => x.updatedBy,
                        principalTable: "users",
                        principalColumn: "userid");
                });

            migrationBuilder.CreateTable(
                name: "log",
                schema: "Swimmers",
                columns: table => new
                {
                    LID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    swimmerID = table.Column<long>(type: "bigint", nullable: false),
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    createdAtsite = table.Column<short>(type: "smallint", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAtDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__log__C6555721C4CDAF7A", x => x.LID);
                    table.ForeignKey(
                        name: "FK__log__ActionID__038683F8",
                        column: x => x.ActionID,
                        principalTable: "Actions",
                        principalColumn: "ActionID");
                    table.ForeignKey(
                        name: "FK__log__CreatedBy__056ECC6A",
                        column: x => x.CreatedBy,
                        principalTable: "users",
                        principalColumn: "userid");
                    table.ForeignKey(
                        name: "FK__log__createdAtsi__047AA831",
                        column: x => x.createdAtsite,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__log__swimmerID__0662F0A3",
                        column: x => x.swimmerID,
                        principalSchema: "Swimmers",
                        principalTable: "Info",
                        principalColumn: "SwimmerID");
                });

            migrationBuilder.CreateTable(
                name: "Parent",
                schema: "Swimmers",
                columns: table => new
                {
                    SwimmerID = table.Column<long>(type: "bigint", nullable: false),
                    SwimmerName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    PrimaryPhone = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    SecondaryPhone = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    PrimaryJop = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SecondaryJop = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    MemberOf = table.Column<short>(type: "smallint", nullable: true),
                    DiscountRate = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Parent__E8BD7A65A62F86BB", x => x.SwimmerID);
                    table.ForeignKey(
                        name: "FK__Parent__MemberOf__075714DC",
                        column: x => x.MemberOf,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Parent__SwimmerI__084B3915",
                        column: x => x.SwimmerID,
                        principalSchema: "Swimmers",
                        principalTable: "Info",
                        principalColumn: "SwimmerID");
                });

            migrationBuilder.CreateTable(
                name: "Technical",
                schema: "Swimmers",
                columns: table => new
                {
                    SwimmerID = table.Column<long>(type: "bigint", nullable: false),
                    Site = table.Column<short>(type: "smallint", nullable: false),
                    CurrentLevel = table.Column<short>(type: "smallint", nullable: false),
                    ISSchool = table.Column<bool>(type: "bit", nullable: true),
                    ISPreTeam = table.Column<bool>(type: "bit", nullable: true),
                    LastStar = table.Column<short>(type: "smallint", nullable: true),
                    ISTeam = table.Column<bool>(type: "bit", nullable: true),
                    IsShort = table.Column<bool>(type: "bit", nullable: true),
                    FirstSP = table.Column<short>(type: "smallint", nullable: true),
                    SecondSP = table.Column<short>(type: "smallint", nullable: true),
                    IsIM = table.Column<bool>(type: "bit", nullable: true),
                    IsFs = table.Column<bool>(type: "bit", nullable: true),
                    IsLong = table.Column<bool>(type: "bit", nullable: true),
                    IsFins = table.Column<bool>(type: "bit", nullable: true),
                    IsMono = table.Column<bool>(type: "bit", nullable: true),
                    Addedby = table.Column<int>(type: "int", nullable: false),
                    AddedAt = table.Column<DateOnly>(type: "date", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateOnly>(type: "date", nullable: true),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Technica__E8BD7A65C169533B", x => x.SwimmerID);
                    table.ForeignKey(
                        name: "FK__Technical__Added__093F5D4E",
                        column: x => x.Addedby,
                        principalTable: "users",
                        principalColumn: "userid");
                    table.ForeignKey(
                        name: "FK__Technical__Curre__0A338187",
                        column: x => x.CurrentLevel,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Technical__First__0B27A5C0",
                        column: x => x.FirstSP,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Technical__LastS__0C1BC9F9",
                        column: x => x.LastStar,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Technical__Secon__0D0FEE32",
                        column: x => x.SecondSP,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Technical__Site__0E04126B",
                        column: x => x.Site,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Technical__Swimm__0D7A0286",
                        column: x => x.SwimmerID,
                        principalSchema: "Swimmers",
                        principalTable: "Info",
                        principalColumn: "SwimmerID");
                    table.ForeignKey(
                        name: "FK__Technical__Updat__0FEC5ADD",
                        column: x => x.UpdatedBy,
                        principalTable: "users",
                        principalColumn: "userid");
                });

            migrationBuilder.CreateTable(
                name: "Times",
                schema: "Swimmers",
                columns: table => new
                {
                    TID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SwimmerID = table.Column<long>(type: "bigint", nullable: false),
                    Site = table.Column<short>(type: "smallint", nullable: false),
                    RaceID = table.Column<short>(type: "smallint", nullable: false),
                    Time = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    RacePlaceID = table.Column<short>(type: "smallint", nullable: false),
                    RaceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    AddedBY = table.Column<int>(type: "int", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Times__C456D7296CF66AEE", x => x.TID);
                    table.ForeignKey(
                        name: "FK__Times__AddedBY__18EBB532",
                        column: x => x.AddedBY,
                        principalTable: "users",
                        principalColumn: "userid");
                    table.ForeignKey(
                        name: "FK__Times__RaceID__17036CC0",
                        column: x => x.RaceID,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Times__RacePlace__17F790F9",
                        column: x => x.RacePlaceID,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Times__Site__160F4887",
                        column: x => x.Site,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Times__SwimmerID__151B244E",
                        column: x => x.SwimmerID,
                        principalSchema: "Swimmers",
                        principalTable: "Info",
                        principalColumn: "SwimmerID");
                });

            migrationBuilder.CreateTable(
                name: "trans",
                columns: table => new
                {
                    TID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    swimmerID = table.Column<long>(type: "bigint", nullable: false),
                    action = table.Column<int>(type: "int", nullable: false),
                    site = table.Column<short>(type: "smallint", nullable: false),
                    itemID = table.Column<int>(type: "int", nullable: false),
                    productID = table.Column<short>(type: "smallint", nullable: false),
                    DurationInMonth = table.Column<short>(type: "smallint", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "money", nullable: false),
                    createdBy = table.Column<int>(type: "int", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__trans__C456D729C2A6D762", x => x.TID);
                    table.ForeignKey(
                        name: "FK__trans__action__4F12BBB9",
                        column: x => x.action,
                        principalTable: "Actions",
                        principalColumn: "ActionID");
                    table.ForeignKey(
                        name: "FK__trans__createdBy__5006DFF2",
                        column: x => x.createdBy,
                        principalTable: "users",
                        principalColumn: "userid");
                    table.ForeignKey(
                        name: "FK__trans__itemID__50FB042B",
                        column: x => x.itemID,
                        principalTable: "Invoice_Item",
                        principalColumn: "ItemID");
                    table.ForeignKey(
                        name: "FK__trans__productID__51EF2864",
                        column: x => x.productID,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__trans__site__52E34C9D",
                        column: x => x.site,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__trans__swimmerID__53D770D6",
                        column: x => x.swimmerID,
                        principalSchema: "Swimmers",
                        principalTable: "Info",
                        principalColumn: "SwimmerID");
                });

            migrationBuilder.CreateTable(
                name: "Details",
                schema: "Schools",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolID = table.Column<long>(type: "bigint", nullable: false),
                    SwimmerID = table.Column<long>(type: "bigint", nullable: false),
                    CoachID = table.Column<int>(type: "int", nullable: false),
                    SwimmerLevel = table.Column<short>(type: "smallint", nullable: false),
                    site = table.Column<short>(type: "smallint", nullable: false),
                    Attendence = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Details__3214EC270D648956", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Details__CoachID__2DE6D218",
                        column: x => x.CoachID,
                        principalTable: "Coaches",
                        principalColumn: "CoachID");
                    table.ForeignKey(
                        name: "FK__Details__SchoolI__2BFE89A6",
                        column: x => x.SchoolID,
                        principalSchema: "Schools",
                        principalTable: "info",
                        principalColumn: "SchoolID");
                    table.ForeignKey(
                        name: "FK__Details__Swimmer__2CF2ADDF",
                        column: x => x.SwimmerID,
                        principalSchema: "Swimmers",
                        principalTable: "Info",
                        principalColumn: "SwimmerID");
                    table.ForeignKey(
                        name: "FK__Details__Swimmer__2EDAF651",
                        column: x => x.SwimmerLevel,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Details__site__2FCF1A8A",
                        column: x => x.site,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                });

            migrationBuilder.CreateTable(
                name: "Ended",
                schema: "Schools",
                columns: table => new
                {
                    Schoolid = table.Column<long>(type: "bigint", nullable: false),
                    EndedAt = table.Column<DateOnly>(type: "date", nullable: false),
                    EndedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__Ended__EndedBy__6CA31EA0",
                        column: x => x.EndedBy,
                        principalTable: "users",
                        principalColumn: "userid");
                    table.ForeignKey(
                        name: "FK__Ended__Schoolid__6D9742D9",
                        column: x => x.Schoolid,
                        principalSchema: "Schools",
                        principalTable: "info",
                        principalColumn: "SchoolID");
                });

            migrationBuilder.CreateTable(
                name: "log",
                schema: "Schools",
                columns: table => new
                {
                    LID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schoolID = table.Column<long>(type: "bigint", nullable: false),
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    swimmerID = table.Column<long>(type: "bigint", nullable: true),
                    createdAtsite = table.Column<short>(type: "smallint", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAtDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__log__C6555721D313A0E7", x => x.LID);
                    table.ForeignKey(
                        name: "FK__log__ActionID__73852659",
                        column: x => x.ActionID,
                        principalTable: "Actions",
                        principalColumn: "ActionID");
                    table.ForeignKey(
                        name: "FK__log__CreatedBy__76619304",
                        column: x => x.CreatedBy,
                        principalTable: "users",
                        principalColumn: "userid");
                    table.ForeignKey(
                        name: "FK__log__createdAtsi__756D6ECB",
                        column: x => x.createdAtsite,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__log__schoolID__72910220",
                        column: x => x.schoolID,
                        principalSchema: "Schools",
                        principalTable: "info",
                        principalColumn: "SchoolID");
                    table.ForeignKey(
                        name: "FK__log__swimmerID__74794A92",
                        column: x => x.swimmerID,
                        principalSchema: "Swimmers",
                        principalTable: "Info",
                        principalColumn: "SwimmerID");
                });

            migrationBuilder.CreateTable(
                name: "Details",
                schema: "PreTeam",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PTeamID = table.Column<long>(type: "bigint", nullable: false),
                    SwimmerID = table.Column<long>(type: "bigint", nullable: false),
                    CoachID = table.Column<int>(type: "int", nullable: false),
                    SwimmerLevel = table.Column<short>(type: "smallint", nullable: false),
                    LastStar = table.Column<short>(type: "smallint", nullable: false),
                    site = table.Column<short>(type: "smallint", nullable: false),
                    Attendence = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Details__3214EC2716204D81", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Details__CoachID__43D61337",
                        column: x => x.CoachID,
                        principalTable: "Coaches",
                        principalColumn: "CoachID");
                    table.ForeignKey(
                        name: "FK__Details__PTeamID__41EDCAC5",
                        column: x => x.PTeamID,
                        principalSchema: "PreTeam",
                        principalTable: "Info",
                        principalColumn: "PTeamID");
                    table.ForeignKey(
                        name: "FK__Details__Swimmer__42E1EEFE",
                        column: x => x.SwimmerID,
                        principalSchema: "Swimmers",
                        principalTable: "Info",
                        principalColumn: "SwimmerID");
                    table.ForeignKey(
                        name: "FK__Details__Swimmer__44CA3770",
                        column: x => x.SwimmerLevel,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__Details__site__45BE5BA9",
                        column: x => x.site,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                });

            migrationBuilder.CreateTable(
                name: "Ended",
                schema: "PreTeam",
                columns: table => new
                {
                    PTeamID = table.Column<long>(type: "bigint", nullable: false),
                    EndedAt = table.Column<DateOnly>(type: "date", nullable: false),
                    EndedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__Ended__EndedBy__5D60DB10",
                        column: x => x.EndedBy,
                        principalTable: "users",
                        principalColumn: "userid");
                    table.ForeignKey(
                        name: "FK__Ended__PTeamID__5E54FF49",
                        column: x => x.PTeamID,
                        principalSchema: "PreTeam",
                        principalTable: "Info",
                        principalColumn: "PTeamID");
                });

            migrationBuilder.CreateTable(
                name: "log",
                schema: "PreTeam",
                columns: table => new
                {
                    LID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PteamID = table.Column<long>(type: "bigint", nullable: false),
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    SwimmerID = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAtSite = table.Column<short>(type: "smallint", nullable: false),
                    createdby = table.Column<int>(type: "int", nullable: false),
                    createdAtDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__log__C65557219DB6DF1E", x => x.LID);
                    table.ForeignKey(
                        name: "FK__log__ActionID__65F62111",
                        column: x => x.ActionID,
                        principalTable: "Actions",
                        principalColumn: "ActionID");
                    table.ForeignKey(
                        name: "FK__log__CreatedAtSi__66EA454A",
                        column: x => x.CreatedAtSite,
                        principalTable: "AppCodes",
                        principalColumn: "sub_id");
                    table.ForeignKey(
                        name: "FK__log__PteamID__68D28DBC",
                        column: x => x.PteamID,
                        principalSchema: "PreTeam",
                        principalTable: "Info",
                        principalColumn: "PTeamID");
                    table.ForeignKey(
                        name: "FK__log__SwimmerID__69C6B1F5",
                        column: x => x.SwimmerID,
                        principalSchema: "Swimmers",
                        principalTable: "Info",
                        principalColumn: "SwimmerID");
                    table.ForeignKey(
                        name: "FK__log__createdby__67DE6983",
                        column: x => x.createdby,
                        principalTable: "users",
                        principalColumn: "userid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_CoachType",
                table: "Coaches",
                column: "CoachType");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_createdAtSite",
                table: "Coaches",
                column: "createdAtSite");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_createdBy",
                table: "Coaches",
                column: "createdBy");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_Gender",
                table: "Coaches",
                column: "Gender");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_site",
                table: "Coaches",
                column: "site");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_updatedAtSite",
                table: "Coaches",
                column: "updatedAtSite");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_updatedBy",
                table: "Coaches",
                column: "updatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Details_CoachID",
                schema: "PreTeam",
                table: "Details",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_Details_PTeamID",
                schema: "PreTeam",
                table: "Details",
                column: "PTeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Details_site",
                schema: "PreTeam",
                table: "Details",
                column: "site");

            migrationBuilder.CreateIndex(
                name: "IX_Details_SwimmerID",
                schema: "PreTeam",
                table: "Details",
                column: "SwimmerID");

            migrationBuilder.CreateIndex(
                name: "IX_Details_SwimmerLevel",
                schema: "PreTeam",
                table: "Details",
                column: "SwimmerLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Details_CoachID",
                schema: "Schools",
                table: "Details",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_Details_SchoolID",
                schema: "Schools",
                table: "Details",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Details_site",
                schema: "Schools",
                table: "Details",
                column: "site");

            migrationBuilder.CreateIndex(
                name: "IX_Details_SwimmerID",
                schema: "Schools",
                table: "Details",
                column: "SwimmerID");

            migrationBuilder.CreateIndex(
                name: "IX_Details_SwimmerLevel",
                schema: "Schools",
                table: "Details",
                column: "SwimmerLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Ended_EndedBy",
                schema: "PreTeam",
                table: "Ended",
                column: "EndedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Ended_PTeamID",
                schema: "PreTeam",
                table: "Ended",
                column: "PTeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Ended_EndedBy",
                schema: "Schools",
                table: "Ended",
                column: "EndedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Ended_Schoolid",
                schema: "Schools",
                table: "Ended",
                column: "Schoolid");

            migrationBuilder.CreateIndex(
                name: "IX_info_CoachID",
                schema: "Schools",
                table: "info",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_info_createdAtSite",
                schema: "Schools",
                table: "info",
                column: "createdAtSite");

            migrationBuilder.CreateIndex(
                name: "IX_info_createdBy",
                schema: "Schools",
                table: "info",
                column: "createdBy");

            migrationBuilder.CreateIndex(
                name: "IX_info_schoolLevel",
                schema: "Schools",
                table: "info",
                column: "schoolLevel");

            migrationBuilder.CreateIndex(
                name: "IX_info_SchoolType",
                schema: "Schools",
                table: "info",
                column: "SchoolType");

            migrationBuilder.CreateIndex(
                name: "IX_info_site",
                schema: "Schools",
                table: "info",
                column: "site");

            migrationBuilder.CreateIndex(
                name: "IX_info_updatedAtSite",
                schema: "Schools",
                table: "info",
                column: "updatedAtSite");

            migrationBuilder.CreateIndex(
                name: "IX_info_updatedBy",
                schema: "Schools",
                table: "info",
                column: "updatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Info_CoachID",
                schema: "PreTeam",
                table: "Info",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_Info_createdAtSite",
                schema: "PreTeam",
                table: "Info",
                column: "createdAtSite");

            migrationBuilder.CreateIndex(
                name: "IX_Info_createdBy",
                schema: "PreTeam",
                table: "Info",
                column: "createdBy");

            migrationBuilder.CreateIndex(
                name: "IX_Info_PTeamLevel",
                schema: "PreTeam",
                table: "Info",
                column: "PTeamLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Info_site",
                schema: "PreTeam",
                table: "Info",
                column: "site");

            migrationBuilder.CreateIndex(
                name: "IX_Info_updatedAtSite",
                schema: "PreTeam",
                table: "Info",
                column: "updatedAtSite");

            migrationBuilder.CreateIndex(
                name: "IX_Info_updatedBy",
                schema: "PreTeam",
                table: "Info",
                column: "updatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Info_CLub",
                schema: "Swimmers",
                table: "Info",
                column: "CLub");

            migrationBuilder.CreateIndex(
                name: "IX_Info_CreatedAtSite",
                schema: "Swimmers",
                table: "Info",
                column: "CreatedAtSite");

            migrationBuilder.CreateIndex(
                name: "IX_Info_CreatedBy",
                schema: "Swimmers",
                table: "Info",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Info_CurrentLevel",
                schema: "Swimmers",
                table: "Info",
                column: "CurrentLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Info_Gender",
                schema: "Swimmers",
                table: "Info",
                column: "Gender");

            migrationBuilder.CreateIndex(
                name: "IX_Info_Site",
                schema: "Swimmers",
                table: "Info",
                column: "Site");

            migrationBuilder.CreateIndex(
                name: "IX_Info_StartLevel",
                schema: "Swimmers",
                table: "Info",
                column: "StartLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Info_UpdatedAtSite",
                schema: "Swimmers",
                table: "Info",
                column: "UpdatedAtSite");

            migrationBuilder.CreateIndex(
                name: "IX_Info_UpdatedBy",
                schema: "Swimmers",
                table: "Info",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Item_ActionID",
                table: "Invoice_Item",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Item_CreatedAtSite",
                table: "Invoice_Item",
                column: "CreatedAtSite");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Item_CreatedBy",
                table: "Invoice_Item",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Item_ProductID",
                table: "Invoice_Item",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Item_Site",
                table: "Invoice_Item",
                column: "Site");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Item_UpdateBy",
                table: "Invoice_Item",
                column: "UpdateBy");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Item_UpdatedAtSite",
                table: "Invoice_Item",
                column: "UpdatedAtSite");

            migrationBuilder.CreateIndex(
                name: "IX_log_ActionID",
                schema: "PreTeam",
                table: "log",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_log_CreatedAtSite",
                schema: "PreTeam",
                table: "log",
                column: "CreatedAtSite");

            migrationBuilder.CreateIndex(
                name: "IX_log_createdby",
                schema: "PreTeam",
                table: "log",
                column: "createdby");

            migrationBuilder.CreateIndex(
                name: "IX_log_PteamID",
                schema: "PreTeam",
                table: "log",
                column: "PteamID");

            migrationBuilder.CreateIndex(
                name: "IX_log_SwimmerID",
                schema: "PreTeam",
                table: "log",
                column: "SwimmerID");

            migrationBuilder.CreateIndex(
                name: "IX_log_ActionID",
                schema: "Schools",
                table: "log",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_log_createdAtsite",
                schema: "Schools",
                table: "log",
                column: "createdAtsite");

            migrationBuilder.CreateIndex(
                name: "IX_log_CreatedBy",
                schema: "Schools",
                table: "log",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_log_schoolID",
                schema: "Schools",
                table: "log",
                column: "schoolID");

            migrationBuilder.CreateIndex(
                name: "IX_log_swimmerID",
                schema: "Schools",
                table: "log",
                column: "swimmerID");

            migrationBuilder.CreateIndex(
                name: "IX_log_ActionID",
                schema: "Swimmers",
                table: "log",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_log_createdAtsite",
                schema: "Swimmers",
                table: "log",
                column: "createdAtsite");

            migrationBuilder.CreateIndex(
                name: "IX_log_CreatedBy",
                schema: "Swimmers",
                table: "log",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_log_swimmerID",
                schema: "Swimmers",
                table: "log",
                column: "swimmerID");

            migrationBuilder.CreateIndex(
                name: "IX_Parent_MemberOf",
                schema: "Swimmers",
                table: "Parent",
                column: "MemberOf");

            migrationBuilder.CreateIndex(
                name: "IX_Technical_Addedby",
                schema: "Swimmers",
                table: "Technical",
                column: "Addedby");

            migrationBuilder.CreateIndex(
                name: "IX_Technical_CurrentLevel",
                schema: "Swimmers",
                table: "Technical",
                column: "CurrentLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Technical_FirstSP",
                schema: "Swimmers",
                table: "Technical",
                column: "FirstSP");

            migrationBuilder.CreateIndex(
                name: "IX_Technical_LastStar",
                schema: "Swimmers",
                table: "Technical",
                column: "LastStar");

            migrationBuilder.CreateIndex(
                name: "IX_Technical_SecondSP",
                schema: "Swimmers",
                table: "Technical",
                column: "SecondSP");

            migrationBuilder.CreateIndex(
                name: "IX_Technical_Site",
                schema: "Swimmers",
                table: "Technical",
                column: "Site");

            migrationBuilder.CreateIndex(
                name: "IX_Technical_UpdatedBy",
                schema: "Swimmers",
                table: "Technical",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Times_AddedBY",
                schema: "Swimmers",
                table: "Times",
                column: "AddedBY");

            migrationBuilder.CreateIndex(
                name: "IX_Times_RaceID",
                schema: "Swimmers",
                table: "Times",
                column: "RaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Times_RacePlaceID",
                schema: "Swimmers",
                table: "Times",
                column: "RacePlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Times_Site",
                schema: "Swimmers",
                table: "Times",
                column: "Site");

            migrationBuilder.CreateIndex(
                name: "IX_Times_SwimmerID",
                schema: "Swimmers",
                table: "Times",
                column: "SwimmerID");

            migrationBuilder.CreateIndex(
                name: "IX_trans_action",
                table: "trans",
                column: "action");

            migrationBuilder.CreateIndex(
                name: "IX_trans_createdBy",
                table: "trans",
                column: "createdBy");

            migrationBuilder.CreateIndex(
                name: "IX_trans_itemID",
                table: "trans",
                column: "itemID");

            migrationBuilder.CreateIndex(
                name: "IX_trans_productID",
                table: "trans",
                column: "productID");

            migrationBuilder.CreateIndex(
                name: "IX_trans_site",
                table: "trans",
                column: "site");

            migrationBuilder.CreateIndex(
                name: "IX_trans_swimmerID",
                table: "trans",
                column: "swimmerID");

            migrationBuilder.CreateIndex(
                name: "IX_users_CreatedBy",
                table: "users",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_users_Site",
                table: "users",
                column: "Site");

            migrationBuilder.CreateIndex(
                name: "IX_users_UserTypeID",
                table: "users",
                column: "UserTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Priv_ActionID",
                table: "Users_Priv",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Priv_UserTypeID",
                table: "Users_Priv",
                column: "UserTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Sites_Site",
                table: "Users_Sites",
                column: "Site");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Sites_UserID",
                table: "Users_Sites",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Details",
                schema: "PreTeam");

            migrationBuilder.DropTable(
                name: "Details",
                schema: "Schools");

            migrationBuilder.DropTable(
                name: "Ended",
                schema: "PreTeam");

            migrationBuilder.DropTable(
                name: "Ended",
                schema: "Schools");

            migrationBuilder.DropTable(
                name: "log",
                schema: "PreTeam");

            migrationBuilder.DropTable(
                name: "log",
                schema: "Schools");

            migrationBuilder.DropTable(
                name: "log",
                schema: "Swimmers");

            migrationBuilder.DropTable(
                name: "Parent",
                schema: "Swimmers");

            migrationBuilder.DropTable(
                name: "Technical",
                schema: "Swimmers");

            migrationBuilder.DropTable(
                name: "Times",
                schema: "Swimmers");

            migrationBuilder.DropTable(
                name: "trans");

            migrationBuilder.DropTable(
                name: "Users_Priv");

            migrationBuilder.DropTable(
                name: "Users_Sites");

            migrationBuilder.DropTable(
                name: "Info",
                schema: "PreTeam");

            migrationBuilder.DropTable(
                name: "info",
                schema: "Schools");

            migrationBuilder.DropTable(
                name: "Invoice_Item");

            migrationBuilder.DropTable(
                name: "Info",
                schema: "Swimmers");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "AppCodes");
        }
    }
}
