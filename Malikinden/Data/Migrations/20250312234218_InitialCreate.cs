using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Markalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markalar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sliderlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Acıklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliderlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Talepler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tur = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talepler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Araclar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModelYili = table.Column<int>(type: "int", nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kilometre = table.Column<int>(type: "int", nullable: false),
                    KasaTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YakitTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VitesTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotorHacmi = table.Column<double>(type: "float", nullable: false),
                    BeygirGucu = table.Column<int>(type: "int", nullable: false),
                    YakitTuketimi = table.Column<double>(type: "float", nullable: false),
                    Resim1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resim2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resim3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SatistaMi = table.Column<bool>(type: "bit", nullable: false),
                    Anasayfa = table.Column<bool>(type: "bit", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MarkaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Araclar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Araclar_Markalar_MarkaId",
                        column: x => x.MarkaId,
                        principalTable: "Markalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Soyadi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Sifre = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kullanicilar_Roller_RolId",
                        column: x => x.RolId,
                        principalTable: "Roller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Musteriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AracId = table.Column<int>(type: "int", nullable: false),
                    TalepId = table.Column<int>(type: "int", nullable: false),
                    Adi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Soyadi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TcNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TelNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notlar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteriler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Musteriler_Araclar_AracId",
                        column: x => x.AracId,
                        principalTable: "Araclar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Musteriler_Talepler_TalepId",
                        column: x => x.TalepId,
                        principalTable: "Talepler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Opsiyonlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AracId = table.Column<int>(type: "int", nullable: false),
                    MusteriId = table.Column<int>(type: "int", nullable: false),
                    OpsiyonFiyati = table.Column<int>(type: "int", nullable: false),
                    OpsiyonTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OpsiyonBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EpostaGonderildiMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opsiyonlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opsiyonlar_Araclar_AracId",
                        column: x => x.AracId,
                        principalTable: "Araclar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Opsiyonlar_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Satislar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AracId = table.Column<int>(type: "int", nullable: false),
                    MusteriId = table.Column<int>(type: "int", nullable: false),
                    SatisFiyati = table.Column<int>(type: "int", nullable: false),
                    SatisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satislar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Satislar_Araclar_AracId",
                        column: x => x.AracId,
                        principalTable: "Araclar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Satislar_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Markalar",
                columns: new[] { "Id", "Adi" },
                values: new object[,]
                {
                    { 1, "Honda" },
                    { 2, "Renault" },
                    { 3, "Volkswagen" },
                    { 4, "Toyota" }
                });

            migrationBuilder.InsertData(
                table: "Roller",
                columns: new[] { "Id", "Adi" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Araclar",
                columns: new[] { "Id", "Anasayfa", "BeygirGucu", "EklenmeTarihi", "Fiyat", "KasaTipi", "Kilometre", "MarkaId", "Model", "ModelYili", "MotorHacmi", "Resim1", "Resim2", "Resim3", "SatistaMi", "VitesTipi", "YakitTipi", "YakitTuketimi" },
                values: new object[,]
                {
                    { 1, true, 136, new DateTime(2025, 3, 13, 2, 42, 18, 595, DateTimeKind.Local).AddTicks(6921), 850000m, "Sedan", 45000, 1, "Civic", 2020, 1.6000000000000001, null, null, null, true, "Otomatik", "Benzin", 5.2000000000000002 },
                    { 2, true, 100, new DateTime(2025, 3, 13, 2, 42, 18, 595, DateTimeKind.Local).AddTicks(6923), 550000m, "Hatchback", 68000, 2, "Clio", 2019, 1.3999999999999999, null, null, null, true, "Manuel", "Dizel", 4.7999999999999998 },
                    { 3, true, 190, new DateTime(2025, 3, 13, 2, 42, 18, 595, DateTimeKind.Local).AddTicks(6925), 1250000m, "Sedan", 25000, 3, "Passat", 2021, 2.0, null, null, null, true, "Otomatik", "Benzin", 7.2000000000000002 },
                    { 4, true, 145, new DateTime(2025, 3, 13, 2, 42, 18, 595, DateTimeKind.Local).AddTicks(6926), 950000m, "Sedan", 15000, 4, "Corolla", 2022, 1.5, null, null, null, true, "Otomatik", "Hibrit", 3.8999999999999999 }
                });

            migrationBuilder.InsertData(
                table: "Kullanicilar",
                columns: new[] { "Id", "Adi", "AktifMi", "EklenmeTarihi", "Email", "KullaniciAdi", "RolId", "Sifre", "Soyadi", "Telefon" },
                values: new object[] { 1, "Admin", true, new DateTime(2025, 3, 13, 2, 42, 18, 595, DateTimeKind.Local).AddTicks(6340), "admin@gmail.com", "xkraltr", 1, "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", "Adminoglu", "0506892852" });

            migrationBuilder.CreateIndex(
                name: "IX_Araclar_MarkaId",
                table: "Araclar",
                column: "MarkaId");

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_RolId",
                table: "Kullanicilar",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Musteriler_AracId",
                table: "Musteriler",
                column: "AracId");

            migrationBuilder.CreateIndex(
                name: "IX_Musteriler_TalepId",
                table: "Musteriler",
                column: "TalepId");

            migrationBuilder.CreateIndex(
                name: "IX_Opsiyonlar_AracId",
                table: "Opsiyonlar",
                column: "AracId");

            migrationBuilder.CreateIndex(
                name: "IX_Opsiyonlar_MusteriId",
                table: "Opsiyonlar",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_Satislar_AracId",
                table: "Satislar",
                column: "AracId");

            migrationBuilder.CreateIndex(
                name: "IX_Satislar_MusteriId",
                table: "Satislar",
                column: "MusteriId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "Opsiyonlar");

            migrationBuilder.DropTable(
                name: "Satislar");

            migrationBuilder.DropTable(
                name: "Sliderlar");

            migrationBuilder.DropTable(
                name: "Roller");

            migrationBuilder.DropTable(
                name: "Musteriler");

            migrationBuilder.DropTable(
                name: "Araclar");

            migrationBuilder.DropTable(
                name: "Talepler");

            migrationBuilder.DropTable(
                name: "Markalar");
        }
    }
}
