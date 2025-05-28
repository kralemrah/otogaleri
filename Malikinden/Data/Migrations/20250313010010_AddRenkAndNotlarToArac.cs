using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRenkAndNotlarToArac : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 1,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 4, 0, 10, 899, DateTimeKind.Local).AddTicks(497));

            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 2,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 4, 0, 10, 899, DateTimeKind.Local).AddTicks(499));

            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 3,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 4, 0, 10, 899, DateTimeKind.Local).AddTicks(501));

            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 4,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 4, 0, 10, 899, DateTimeKind.Local).AddTicks(502));

            migrationBuilder.UpdateData(
                table: "Kullanicilar",
                keyColumn: "Id",
                keyValue: 1,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 4, 0, 10, 898, DateTimeKind.Local).AddTicks(9898));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 1,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 3, 48, 25, 230, DateTimeKind.Local).AddTicks(3160));

            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 2,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 3, 48, 25, 230, DateTimeKind.Local).AddTicks(3161));

            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 3,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 3, 48, 25, 230, DateTimeKind.Local).AddTicks(3185));

            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 4,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 3, 48, 25, 230, DateTimeKind.Local).AddTicks(3187));

            migrationBuilder.UpdateData(
                table: "Kullanicilar",
                keyColumn: "Id",
                keyValue: 1,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 3, 48, 25, 230, DateTimeKind.Local).AddTicks(2448));
        }
    }
}
