using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRenkToArac : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notlar",
                table: "Araclar",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Renk",
                table: "Araclar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EklenmeTarihi", "Notlar", "Renk" },
                values: new object[] { new DateTime(2025, 3, 13, 3, 48, 25, 230, DateTimeKind.Local).AddTicks(3160), null, "Beyaz" });

            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EklenmeTarihi", "Notlar", "Renk" },
                values: new object[] { new DateTime(2025, 3, 13, 3, 48, 25, 230, DateTimeKind.Local).AddTicks(3161), null, "Kırmızı" });

            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EklenmeTarihi", "Notlar", "Renk" },
                values: new object[] { new DateTime(2025, 3, 13, 3, 48, 25, 230, DateTimeKind.Local).AddTicks(3185), null, "Siyah" });

            migrationBuilder.UpdateData(
                table: "Araclar",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EklenmeTarihi", "Notlar", "Renk" },
                values: new object[] { new DateTime(2025, 3, 13, 3, 48, 25, 230, DateTimeKind.Local).AddTicks(3187), null, "Gri" });

            migrationBuilder.UpdateData(
                table: "Kullanicilar",
                keyColumn: "Id",
                keyValue: 1,
                column: "EklenmeTarihi",
                value: new DateTime(2025, 3, 13, 3, 48, 25, 230, DateTimeKind.Local).AddTicks(2448));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notlar",
                table: "Araclar");

            migrationBuilder.DropColumn(
                name: "Renk",
                table: "Araclar");

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
    }
}
