using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BattleCottage.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameModes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameModes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BackgroundImage = table.Column<string>(type: "text", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameStyles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStyles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LFGPostDurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DurationInMinutes = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LFGPostDurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LFGPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DurationInMinutesId = table.Column<int>(type: "integer", nullable: false),
                    GameModeId = table.Column<int>(type: "integer", nullable: false),
                    GameStyleId = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LFGPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LFGPosts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LFGPosts_GameModes_GameModeId",
                        column: x => x.GameModeId,
                        principalTable: "GameModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LFGPosts_GameStyles_GameStyleId",
                        column: x => x.GameStyleId,
                        principalTable: "GameStyles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LFGPosts_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LFGPosts_LFGPostDurations_DurationInMinutesId",
                        column: x => x.DurationInMinutesId,
                        principalTable: "LFGPostDurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LFGPostGameRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LFGPostId = table.Column<int>(type: "integer", nullable: false),
                    GameRoleId = table.Column<int>(type: "integer", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LFGPostGameRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LFGPostGameRoles_GameRoles_GameRoleId",
                        column: x => x.GameRoleId,
                        principalTable: "GameRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LFGPostGameRoles_LFGPosts_LFGPostId",
                        column: x => x.LFGPostId,
                        principalTable: "LFGPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GameModes",
                columns: new[] { "Id", "DateAdded", "DateUpdated", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4457), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4458), "PvP" },
                    { 2, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4459), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4459), "PvE" },
                    { 3, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4460), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4460), "Co-op" },
                    { 4, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4461), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4462), "Multiplayer" },
                    { 5, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4462), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4463), "Battle Royale" },
                    { 6, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4464), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4464), "Other" }
                });

            migrationBuilder.InsertData(
                table: "GameRoles",
                columns: new[] { "Id", "DateAdded", "DateUpdated", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4625), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4625), "Tank" },
                    { 2, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4627), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4627), "Healer" },
                    { 3, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4628), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4629), "DPS" },
                    { 4, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4630), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4630), "Top Lane" },
                    { 5, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4631), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4631), "Bottom Lane" },
                    { 6, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4632), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4633), "Mid Lane" },
                    { 7, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4634), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4634), "Jungle" },
                    { 8, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4635), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4635), "Support" },
                    { 9, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4636), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4637), "Entry Fragger" },
                    { 10, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4638), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4638), "ReFragger" },
                    { 11, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4639), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4639), "Strategy Caller" },
                    { 12, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4640), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4641), "Lurker" },
                    { 13, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4641), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4642), "Awper" },
                    { 14, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4643), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4643), "Combat Support" },
                    { 15, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4644), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4644), "Medic" },
                    { 16, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4645), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4646), "Assault" },
                    { 17, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4647), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4647), "Recon" },
                    { 18, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4648), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4648), "Friendly" },
                    { 19, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4649), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4650), "Funny" },
                    { 20, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4650), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4651), "Serious" },
                    { 21, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4652), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4652), "e-Girl" },
                    { 22, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4653), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4653), "Silent" },
                    { 23, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4654), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4655), "Carry" }
                });

            migrationBuilder.InsertData(
                table: "GameStyles",
                columns: new[] { "Id", "DateAdded", "DateUpdated", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4610), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4611), "Casual" },
                    { 2, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4612), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4612), "Competitive" },
                    { 3, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4613), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4613), "Other" }
                });

            migrationBuilder.InsertData(
                table: "LFGPostDurations",
                columns: new[] { "Id", "DateAdded", "DateUpdated", "DurationInMinutes", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4674), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4675), 60, "1 hour" },
                    { 2, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4676), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4676), 120, "2 hour" },
                    { 3, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4677), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4677), 300, "5 hour" },
                    { 4, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4678), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4679), 720, "12 hour" },
                    { 5, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4680), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4680), 1440, "1 day" },
                    { 6, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4681), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4681), 4320, "3 days" },
                    { 7, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4682), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4683), 10080, "7 days" },
                    { 8, new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4683), new DateTime(2023, 11, 2, 17, 14, 17, 975, DateTimeKind.Utc).AddTicks(4684), 43200, "30 days" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameModes_Name",
                table: "GameModes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameRoles_Name",
                table: "GameRoles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_Name",
                table: "Games",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameStyles_Name",
                table: "GameStyles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LFGPostDurations_DurationInMinutes",
                table: "LFGPostDurations",
                column: "DurationInMinutes",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LFGPostGameRoles_GameRoleId",
                table: "LFGPostGameRoles",
                column: "GameRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_LFGPostGameRoles_LFGPostId",
                table: "LFGPostGameRoles",
                column: "LFGPostId");

            migrationBuilder.CreateIndex(
                name: "IX_LFGPosts_DurationInMinutesId",
                table: "LFGPosts",
                column: "DurationInMinutesId");

            migrationBuilder.CreateIndex(
                name: "IX_LFGPosts_GameId",
                table: "LFGPosts",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_LFGPosts_GameModeId",
                table: "LFGPosts",
                column: "GameModeId");

            migrationBuilder.CreateIndex(
                name: "IX_LFGPosts_GameStyleId",
                table: "LFGPosts",
                column: "GameStyleId");

            migrationBuilder.CreateIndex(
                name: "IX_LFGPosts_UserId",
                table: "LFGPosts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "LFGPostGameRoles");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "GameRoles");

            migrationBuilder.DropTable(
                name: "LFGPosts");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "GameModes");

            migrationBuilder.DropTable(
                name: "GameStyles");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "LFGPostDurations");
        }
    }
}
