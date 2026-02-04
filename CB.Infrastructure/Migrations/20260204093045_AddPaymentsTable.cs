using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CB.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Departments_DepartmentId",
                table: "Branches");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Branches",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Departments_DepartmentId",
                table: "Branches",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Departments_DepartmentId",
                table: "Branches");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Branches",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 10, 29, 19, 268, DateTimeKind.Utc).AddTicks(917), new DateTime(2025, 12, 23, 10, 29, 19, 268, DateTimeKind.Utc).AddTicks(918) });

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 10, 29, 19, 268, DateTimeKind.Utc).AddTicks(920), new DateTime(2025, 12, 23, 10, 29, 19, 268, DateTimeKind.Utc).AddTicks(920) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 10, 29, 18, 910, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 12, 23, 10, 29, 18, 910, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 10, 29, 18, 910, DateTimeKind.Utc).AddTicks(8392), new DateTime(2025, 12, 23, 10, 29, 18, 910, DateTimeKind.Utc).AddTicks(8392) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 10, 29, 18, 910, DateTimeKind.Utc).AddTicks(8393), new DateTime(2025, 12, 23, 10, 29, 18, 910, DateTimeKind.Utc).AddTicks(8394) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 10, 29, 18, 910, DateTimeKind.Utc).AddTicks(8395), new DateTime(2025, 12, 23, 10, 29, 18, 910, DateTimeKind.Utc).AddTicks(8395) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 10, 29, 18, 910, DateTimeKind.Utc).AddTicks(8160), new DateTime(2025, 12, 23, 10, 29, 18, 910, DateTimeKind.Utc).AddTicks(8160) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 10, 29, 18, 910, DateTimeKind.Utc).AddTicks(8163), new DateTime(2025, 12, 23, 10, 29, 18, 910, DateTimeKind.Utc).AddTicks(8164) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 10, 29, 18, 910, DateTimeKind.Utc).AddTicks(8165), new DateTime(2025, 12, 23, 10, 29, 18, 910, DateTimeKind.Utc).AddTicks(8165) });

            migrationBuilder.UpdateData(
                table: "Translates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 10, 29, 19, 268, DateTimeKind.Utc).AddTicks(1065), new DateTime(2025, 12, 23, 10, 29, 19, 268, DateTimeKind.Utc).AddTicks(1065) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 10, 29, 19, 25, DateTimeKind.Utc).AddTicks(9996), "$2a$11$MlN4o9YRS8JknuqJ5zwvmu5w.7vi1QbMmZ8xRRgUiaZmCOgiS6/4q", new DateTime(2025, 12, 23, 10, 29, 19, 26, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 10, 29, 19, 144, DateTimeKind.Utc).AddTicks(6001), "$2a$11$5YfkMm6JNThs933OzxML6esHvDgggiiKdaTXcjeR7sJRrK0FA/3rm", new DateTime(2025, 12, 23, 10, 29, 19, 144, DateTimeKind.Utc).AddTicks(6007) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 23, 10, 29, 19, 267, DateTimeKind.Utc).AddTicks(9709), "$2a$11$m1TYZ/3.M4eoyncoH.2VrOusTofFE3hBeslkGzpdtPgXnD0Ek5uY6", new DateTime(2025, 12, 23, 10, 29, 19, 267, DateTimeKind.Utc).AddTicks(9715) });

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Departments_DepartmentId",
                table: "Branches",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
