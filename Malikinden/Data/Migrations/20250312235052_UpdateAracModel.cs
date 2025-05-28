using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAracModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 1,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 2, 50, 52, 839, DateTimeKind.Local).AddTicks(8524));

            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 2,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 2, 50, 52, 839, DateTimeKind.Local).AddTicks(8527));

            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 3,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 2, 50, 52, 839, DateTimeKind.Local).AddTicks(8528));

            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 4,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 2, 50, 52, 839, DateTimeKind.Local).AddTicks(8530));

            migrationBuilder.UpdateData(
                table: "Kullanicilar",
                keyColumn: "Id",
                keyValue: 1,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 2, 50, 52, 839, DateTimeKind.Local).AddTicks(7942));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 1,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 2, 42, 18, 595, DateTimeKind.Local).AddTicks(6921));

            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 2,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 2, 42, 18, 595, DateTimeKind.Local).AddTicks(6923));

            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 3,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 2, 42, 18, 595, DateTimeKind.Local).AddTicks(6925));

            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 4,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 2, 42, 18, 595, DateTimeKind.Local).AddTicks(6926));

            migrationBuilder.UpdateData(
                table: "Kullanicilar",
                keyColumn: "Id",
                keyValue: 1,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 2, 42, 18, 595, DateTimeKind.Local).AddTicks(6340));
        }
    }
}
