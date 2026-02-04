using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CB.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDigitalPaymentInfographicsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "DigitalPaymentInfographicCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalPaymentInfographicCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DigitalPaymentInfographicCategoryTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DigitalPaymentInfographicCategoryId = table.Column<int>(type: "integer", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalPaymentInfographicCategoryTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalPaymentInfographicCategoryTranslations_DigitalPaymen~",
                        column: x => x.DigitalPaymentInfographicCategoryId,
                        principalTable: "DigitalPaymentInfographicCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DigitalPaymentInfographicCategoryTranslations_Languages_Lan~",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DigitalPaymentInfographics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: true),
                    DigitalPaymentInfographicCategoryId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalPaymentInfographics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalPaymentInfographics_DigitalPaymentInfographicCategor~",
                        column: x => x.DigitalPaymentInfographicCategoryId,
                        principalTable: "DigitalPaymentInfographicCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DigitalPaymentInfographicItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    DigitalPaymentInfographicId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalPaymentInfographicItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalPaymentInfographicItems_DigitalPaymentInfographics_D~",
                        column: x => x.DigitalPaymentInfographicId,
                        principalTable: "DigitalPaymentInfographics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DigitalPaymentInfographicTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DigitalPaymentInfographicId = table.Column<int>(type: "integer", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalPaymentInfographicTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalPaymentInfographicTranslations_DigitalPaymentInfogra~",
                        column: x => x.DigitalPaymentInfographicId,
                        principalTable: "DigitalPaymentInfographics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DigitalPaymentInfographicTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 12, 35, 19, 780, DateTimeKind.Utc).AddTicks(3479), new DateTime(2026, 2, 4, 12, 35, 19, 780, DateTimeKind.Utc).AddTicks(3479) });

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 12, 35, 19, 780, DateTimeKind.Utc).AddTicks(3482), new DateTime(2026, 2, 4, 12, 35, 19, 780, DateTimeKind.Utc).AddTicks(3482) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 12, 35, 19, 397, DateTimeKind.Utc).AddTicks(4004), new DateTime(2026, 2, 4, 12, 35, 19, 397, DateTimeKind.Utc).AddTicks(4005) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 12, 35, 19, 397, DateTimeKind.Utc).AddTicks(4007), new DateTime(2026, 2, 4, 12, 35, 19, 397, DateTimeKind.Utc).AddTicks(4007) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 12, 35, 19, 397, DateTimeKind.Utc).AddTicks(4009), new DateTime(2026, 2, 4, 12, 35, 19, 397, DateTimeKind.Utc).AddTicks(4009) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 12, 35, 19, 397, DateTimeKind.Utc).AddTicks(4010), new DateTime(2026, 2, 4, 12, 35, 19, 397, DateTimeKind.Utc).AddTicks(4010) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 12, 35, 19, 397, DateTimeKind.Utc).AddTicks(3661), new DateTime(2026, 2, 4, 12, 35, 19, 397, DateTimeKind.Utc).AddTicks(3661) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 12, 35, 19, 397, DateTimeKind.Utc).AddTicks(3666), new DateTime(2026, 2, 4, 12, 35, 19, 397, DateTimeKind.Utc).AddTicks(3666) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 12, 35, 19, 397, DateTimeKind.Utc).AddTicks(3667), new DateTime(2026, 2, 4, 12, 35, 19, 397, DateTimeKind.Utc).AddTicks(3668) });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 12, 35, 19, 780, DateTimeKind.Utc).AddTicks(3693), new DateTime(2026, 2, 4, 12, 35, 19, 780, DateTimeKind.Utc).AddTicks(3693) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 12, 35, 19, 528, DateTimeKind.Utc).AddTicks(8672), "$2a$11$kiWpqA7FqgtEUPziPfaomOjJc21uu5aBXd2xBlDZRK55whGWp4X32", new DateTime(2026, 2, 4, 12, 35, 19, 528, DateTimeKind.Utc).AddTicks(8680) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 12, 35, 19, 657, DateTimeKind.Utc).AddTicks(6205), "$2a$11$qwNAISS4rFX.TxDfECFj5uwT0Uz7xLJKGqCxpId7m.AvzFB.s7k02", new DateTime(2026, 2, 4, 12, 35, 19, 657, DateTimeKind.Utc).AddTicks(6210) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 12, 35, 19, 780, DateTimeKind.Utc).AddTicks(2453), "$2a$11$QpHhu1CVXTfRkX8ff8m/XuagEbd1K/0Z72N1LmvJN1ESC5maESveG", new DateTime(2026, 2, 4, 12, 35, 19, 780, DateTimeKind.Utc).AddTicks(2459) });

            migrationBuilder.CreateIndex(
                name: "IX_DigitalPaymentInfographicCategoryTranslations_DigitalPaymen~",
                table: "DigitalPaymentInfographicCategoryTranslations",
                column: "DigitalPaymentInfographicCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalPaymentInfographicCategoryTranslations_LanguageId",
                table: "DigitalPaymentInfographicCategoryTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalPaymentInfographicItems_DigitalPaymentInfographicId",
                table: "DigitalPaymentInfographicItems",
                column: "DigitalPaymentInfographicId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalPaymentInfographics_DigitalPaymentInfographicCategor~",
                table: "DigitalPaymentInfographics",
                column: "DigitalPaymentInfographicCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalPaymentInfographicTranslations_DigitalPaymentInfogra~",
                table: "DigitalPaymentInfographicTranslations",
                column: "DigitalPaymentInfographicId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalPaymentInfographicTranslations_LanguageId",
                table: "DigitalPaymentInfographicTranslations",
                column: "LanguageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DigitalPaymentInfographicCategoryTranslations");

            migrationBuilder.DropTable(
                name: "DigitalPaymentInfographicItems");

            migrationBuilder.DropTable(
                name: "DigitalPaymentInfographicTranslations");

            migrationBuilder.DropTable(
                name: "DigitalPaymentInfographics");

            migrationBuilder.DropTable(
                name: "DigitalPaymentInfographicCategories");

            migrationBuilder.CreateTable(
                name: "DigitalPaymentInfographicCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalPaymentInfographicCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DigitalPaymentInfographicCategoryTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DigitalPaymentInfographicCategoryId = table.Column<int>(type: "integer", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalPaymentInfographicCategoryTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalPaymentInfographicCategoryTranslations_DigitalPayment~",
                        column: x => x.DigitalPaymentInfographicCategoryId,
                        principalTable: "DigitalPaymentInfographicCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DigitalPaymentInfographicCategoryTranslations_Languages_Lang~",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DigitalPaymentInfographics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DigitalPaymentInfographicCategoryId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalPaymentInfographics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalPaymentInfographics_DigitalPaymentInfographicCategorie~",
                        column: x => x.DigitalPaymentInfographicCategoryId,
                        principalTable: "DigitalPaymentInfographicCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DigitalPaymentInfographicItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DigitalPaymentInfographicId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalPaymentInfographicItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalPaymentInfographicItems_DigitalPaymentInfographics_Dig~",
                        column: x => x.DigitalPaymentInfographicId,
                        principalTable: "DigitalPaymentInfographics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DigitalPaymentInfographicTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DigitalPaymentInfographicId = table.Column<int>(type: "integer", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalPaymentInfographicTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalPaymentInfographicTranslations_DigitalPaymentInfograh~",
                        column: x => x.DigitalPaymentInfographicId,
                        principalTable: "DigitalPaymentInfographics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DigitalPaymentInfographicTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 30, 44, 306, DateTimeKind.Utc).AddTicks(8581), new DateTime(2026, 2, 4, 9, 30, 44, 306, DateTimeKind.Utc).AddTicks(8581) });

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 30, 44, 306, DateTimeKind.Utc).AddTicks(8584), new DateTime(2026, 2, 4, 9, 30, 44, 306, DateTimeKind.Utc).AddTicks(8584) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 30, 43, 931, DateTimeKind.Utc).AddTicks(9514), new DateTime(2026, 2, 4, 9, 30, 43, 931, DateTimeKind.Utc).AddTicks(9514) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 30, 43, 931, DateTimeKind.Utc).AddTicks(9516), new DateTime(2026, 2, 4, 9, 30, 43, 931, DateTimeKind.Utc).AddTicks(9516) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 30, 43, 931, DateTimeKind.Utc).AddTicks(9517), new DateTime(2026, 2, 4, 9, 30, 43, 931, DateTimeKind.Utc).AddTicks(9518) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 30, 43, 931, DateTimeKind.Utc).AddTicks(9519), new DateTime(2026, 2, 4, 9, 30, 43, 931, DateTimeKind.Utc).AddTicks(9519) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 30, 43, 931, DateTimeKind.Utc).AddTicks(9290), new DateTime(2026, 2, 4, 9, 30, 43, 931, DateTimeKind.Utc).AddTicks(9291) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 30, 43, 931, DateTimeKind.Utc).AddTicks(9294), new DateTime(2026, 2, 4, 9, 30, 43, 931, DateTimeKind.Utc).AddTicks(9294) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 30, 43, 931, DateTimeKind.Utc).AddTicks(9297), new DateTime(2026, 2, 4, 9, 30, 43, 931, DateTimeKind.Utc).AddTicks(9297) });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 30, 44, 306, DateTimeKind.Utc).AddTicks(8721), new DateTime(2026, 2, 4, 9, 30, 44, 306, DateTimeKind.Utc).AddTicks(8722) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 30, 44, 55, DateTimeKind.Utc).AddTicks(7035), "$2a$11$d2DbSSk16ClCRU0pOkRZgO/PLPQWCssR5j9HD8kWJyOkyv5N98J2i", new DateTime(2026, 2, 4, 9, 30, 44, 55, DateTimeKind.Utc).AddTicks(7040) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 30, 44, 181, DateTimeKind.Utc).AddTicks(7609), "$2a$11$dQR9aGi.bErVtza.bYaL4.MaY1QZRVFyacruBJAqNGXmJKmFUXuLi", new DateTime(2026, 2, 4, 9, 30, 44, 181, DateTimeKind.Utc).AddTicks(7613) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 4, 9, 30, 44, 306, DateTimeKind.Utc).AddTicks(7662), "$2a$11$BeBib92QZPGnTBG0GAp74enGQvUsmpLwiYn7crekjwray/Sw1wOQK", new DateTime(2026, 2, 4, 9, 30, 44, 306, DateTimeKind.Utc).AddTicks(7666) });

            migrationBuilder.CreateIndex(
                name: "IX_DigitalPaymentInfographicCategoryTranslations_DigitalPayment~",
                table: "DigitalPaymentInfographicCategoryTranslations",
                column: "DigitalPaymentInfographicCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalPaymentInfographicCategoryTranslations_LanguageId",
                table: "DigitalPaymentInfographicCategoryTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalPaymentInfographicItems_DigitalPaymentInfographicId",
                table: "DigitalPaymentInfographicItems",
                column: "DigitalPaymentInfographicId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalPaymentInfographics_DigitalPaymentInfographicCategoryId",
                table: "DigitalPaymentInfographics",
                column: "DigitalPaymentInfographicCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalPaymentInfographicTranslations_DigitalPaymentInfograh~",
                table: "DigitalPaymentInfographicTranslations",
                column: "DigitalPaymentInfographicId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalPaymentInfographicTranslations_LanguageId",
                table: "DigitalPaymentInfographicTranslations",
                column: "LanguageId");
        }
    }
}
