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
                name: "LfgPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DurationInMinutes = table.Column<int>(type: "integer", nullable: false),
                    GameModeId = table.Column<int>(type: "integer", nullable: false),
                    GameStyleId = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LfgPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LfgPosts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LfgPosts_GameModes_GameModeId",
                        column: x => x.GameModeId,
                        principalTable: "GameModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LfgPosts_GameStyles_GameStyleId",
                        column: x => x.GameStyleId,
                        principalTable: "GameStyles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LfgPosts_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LfgPostGameRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LfgPostId = table.Column<int>(type: "integer", nullable: false),
                    GameRoleId = table.Column<int>(type: "integer", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LfgPostGameRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LfgPostGameRoles_GameRoles_GameRoleId",
                        column: x => x.GameRoleId,
                        principalTable: "GameRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LfgPostGameRoles_LfgPosts_LfgPostId",
                        column: x => x.LfgPostId,
                        principalTable: "LfgPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GameModes",
                columns: new[] { "Id", "DateAdded", "DateUpdated", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(2950), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(2951), "PvP" },
                    { 2, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(2952), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(2953), "PvE" }
                });

            migrationBuilder.InsertData(
                table: "GameRoles",
                columns: new[] { "Id", "DateAdded", "DateUpdated", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3094), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3095), "Tank" },
                    { 2, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3096), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3096), "Healer" },
                    { 3, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3097), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3097), "DPS" },
                    { 4, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3098), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3099), "Top Lane" },
                    { 5, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3100), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3100), "Bottom Lane" },
                    { 6, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3101), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3101), "Mid Lane" },
                    { 7, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3102), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3103), "Jungle" },
                    { 8, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3103), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3104), "Support" },
                    { 9, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3105), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3105), "Entry Fragger" },
                    { 10, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3106), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3106), "ReFragger" },
                    { 11, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3107), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3108), "Strategy Caller" },
                    { 12, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3109), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3109), "Lurker" },
                    { 13, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3110), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3110), "Awper" },
                    { 14, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3111), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3112), "Combat Support" },
                    { 15, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3113), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3113), "Medic" },
                    { 16, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3114), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3114), "Assault" },
                    { 17, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3115), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3115), "Recon" },
                    { 18, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3116), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3117), "Friendly" },
                    { 19, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3118), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3118), "Funny" },
                    { 20, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3119), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3119), "Serious" },
                    { 21, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3120), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3121), "e-Girl" },
                    { 22, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3122), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3122), "Silent" },
                    { 23, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3123), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3123), "Carry" }
                });

            migrationBuilder.InsertData(
                table: "GameStyles",
                columns: new[] { "Id", "DateAdded", "DateUpdated", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3080), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3080), "Casual" },
                    { 2, new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3081), new DateTime(2023, 10, 1, 11, 57, 14, 322, DateTimeKind.Utc).AddTicks(3081), "Competitive" }
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
                name: "IX_LfgPostGameRoles_GameRoleId",
                table: "LfgPostGameRoles",
                column: "GameRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_LfgPostGameRoles_LfgPostId",
                table: "LfgPostGameRoles",
                column: "LfgPostId");

            migrationBuilder.CreateIndex(
                name: "IX_LfgPosts_GameId",
                table: "LfgPosts",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_LfgPosts_GameModeId",
                table: "LfgPosts",
                column: "GameModeId");

            migrationBuilder.CreateIndex(
                name: "IX_LfgPosts_GameStyleId",
                table: "LfgPosts",
                column: "GameStyleId");

            migrationBuilder.CreateIndex(
                name: "IX_LfgPosts_UserId",
                table: "LfgPosts",
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
                name: "LfgPostGameRoles");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "GameRoles");

            migrationBuilder.DropTable(
                name: "LfgPosts");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "GameModes");

            migrationBuilder.DropTable(
                name: "GameStyles");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
