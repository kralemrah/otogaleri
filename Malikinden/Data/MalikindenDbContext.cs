using Core.Helpers;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class MalikindenDbContext : DbContext
    {
        public DbSet<Arac> Araclar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Marka> Markalar { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Rol> Roller { get; set; }
        public DbSet<Satis> Satislar { get; set; }
        public DbSet<Slider> Sliderlar { get; set; }
        public DbSet<Opsiyon> Opsiyonlar { get; set; }
        public DbSet<Talep> Talepler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=EMRAH\SQLEXPRESS;Database=DbMalikinden;Trusted_Connection=True;TrustServerCertificate=True");
            optionsBuilder.UseLazyLoadingProxies(true);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Marka>().Property(m => m.Adi).IsRequired().HasColumnType("varchar(50)");

            modelBuilder.Entity<Rol>().HasData(new Rol
            {
                Id = 1,
                Adi = "Admin"
            });

            modelBuilder.Entity<Kullanici>().HasData(new Kullanici
            {
                Id = 1,
                Adi = "Admin",
                Soyadi = "Adminoglu",
                AktifMi = true,
                EklenmeTarihi = DateTime.Now,
                Telefon = "0506892852",
                Email = "admin@gmail.com",
                KullaniciAdi = "xkraltr",
                Sifre = HashHelper.ComputeSha256Hash("12345"),
                RolId = 1
            });

            // Örnek araçlar
            modelBuilder.Entity<Arac>().HasData(
                new Arac
                {
                    Id = 1,
                    MarkaId = 1,
                    Model = "Civic",
                    Fiyat = 850000,
                    KasaTipi = "Sedan",
                    ModelYili = 2020,
                    Kilometre = 45000,
                    SatistaMi = true,
                    Anasayfa = true,
                    YakitTipi = "Benzin",
                    VitesTipi = "Otomatik",
                    MotorHacmi = 1.6,
                    BeygirGucu = 136,
                    YakitTuketimi = 5.2,
                    EklenmeTarihi = DateTime.Now,
                    Renk = "Beyaz"
                },
                new Arac
                {
                    Id = 2,
                    MarkaId = 2,
                    Model = "Clio",
                    Fiyat = 550000,
                    KasaTipi = "Hatchback",
                    ModelYili = 2019,
                    Kilometre = 68000,
                    SatistaMi = true,
                    Anasayfa = true,
                    YakitTipi = "Dizel",
                    VitesTipi = "Manuel",
                    MotorHacmi = 1.4,
                    BeygirGucu = 100,
                    YakitTuketimi = 4.8,
                    EklenmeTarihi = DateTime.Now,
                    Renk = "Kırmızı"
                },
                new Arac
                {
                    Id = 3,
                    MarkaId = 3,
                    Model = "Passat",
                    Fiyat = 1250000,
                    KasaTipi = "Sedan",
                    ModelYili = 2021,
                    Kilometre = 25000,
                    SatistaMi = true,
                    Anasayfa = true,
                    YakitTipi = "Benzin",
                    VitesTipi = "Otomatik",
                    MotorHacmi = 2.0,
                    BeygirGucu = 190,
                    YakitTuketimi = 7.2,
                    EklenmeTarihi = DateTime.Now,
                    Renk = "Siyah"
                },
                new Arac
                {
                    Id = 4,
                    MarkaId = 4,
                    Model = "Corolla",
                    Fiyat = 950000,
                    KasaTipi = "Sedan",
                    ModelYili = 2022,
                    Kilometre = 15000,
                    SatistaMi = true,
                    Anasayfa = true,
                    YakitTipi = "Hibrit",
                    VitesTipi = "Otomatik",
                    MotorHacmi = 1.5,
                    BeygirGucu = 145,
                    YakitTuketimi = 3.9,
                    EklenmeTarihi = DateTime.Now,
                    Renk = "Gri"
                }
            );

            // Örnek markalar
            modelBuilder.Entity<Marka>().HasData(
                new Marka { Id = 1, Adi = "Honda" },
                new Marka { Id = 2, Adi = "Renault" },
                new Marka { Id = 3, Adi = "Volkswagen" },
                new Marka { Id = 4, Adi = "Toyota" }
            );

            modelBuilder.Entity<Slider>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Opsiyon>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Opsiyon>()
                .HasOne(o => o.Musteri)
                .WithMany(m => m.Opsiyonlar)
                .HasForeignKey(o => o.MusteriId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Satis>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Satis>()
                .HasOne(s => s.Musteri)
                .WithMany()
                .HasForeignKey(s => s.MusteriId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
