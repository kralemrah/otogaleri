using Data;
using Malikinden.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Malikinden.Services
{
    public class AracOneriSistemi
    {
        private readonly MalikindenDbContext _context;

        public AracOneriSistemi(MalikindenDbContext context)
        {
            _context = context;
        }

        private (decimal minFiyat, decimal maxFiyat) ParseFiyatAraligi(string fiyatAraligi)
        {
            // Fiyat aralığını temizle
            fiyatAraligi = fiyatAraligi.Replace(" ", "").Replace(".", "").Replace("TL", "").Trim();
            
            // Fiyat aralığını parçala
            var parcalar = fiyatAraligi.Split('-');
            if (parcalar.Length != 2)
            {
                throw new ArgumentException("Geçersiz fiyat aralığı formatı");
            }

            // Minimum ve maksimum fiyatları parse et
            if (!decimal.TryParse(parcalar[0], out decimal minFiyat) || 
                !decimal.TryParse(parcalar[1], out decimal maxFiyat))
            {
                throw new ArgumentException("Geçersiz fiyat değerleri");
            }

            return (minFiyat, maxFiyat);
        }

        public double HesaplaPuan(Entity.Arac arac, AracOneriViewModel model)
        {
            double puan = 0;
            int kriterSayisi = 0;

            // Fiyat puanı (20 puan)
            if (!string.IsNullOrEmpty(model.FiyatAraligi))
            {
                try
                {
                    var (minFiyat, maxFiyat) = ParseFiyatAraligi(model.FiyatAraligi);
                    var ortalamaFiyat = (double)(minFiyat + maxFiyat) / 2;
                    var fiyatFarki = Math.Abs((double)arac.Fiyat - ortalamaFiyat);
                    var fiyatPuan = 20 - (fiyatFarki / ortalamaFiyat * 20);
                    puan += Math.Max(0, fiyatPuan);
                    kriterSayisi++;
                }
                catch { }
            }

            // Model yılı puanı (20 puan)
            if (!string.IsNullOrEmpty(model.ModelYili))
            {
                if (int.TryParse(model.ModelYili, out int modelYili))
                {
                    var yilFarki = Math.Abs(arac.ModelYili - modelYili);
                    var yilPuan = 20 - (yilFarki * 2);
                    puan += Math.Max(0, yilPuan);
                    kriterSayisi++;
                }
            }

            // Kilometre puanı (20 puan)
            var kilometrePuan = 20 - ((double)arac.Kilometre / 100000 * 20);
            puan += Math.Max(0, kilometrePuan);
            kriterSayisi++;

            // Yakıt tipi puanı (20 puan)
            if (!string.IsNullOrEmpty(model.YakitTipi))
            {
                if (arac.YakitTipi == model.YakitTipi)
                {
                    puan += 20;
                }
                kriterSayisi++;
            }

            // Vites tipi puanı (20 puan)
            if (!string.IsNullOrEmpty(model.VitesTipi))
            {
                if (arac.VitesTipi == model.VitesTipi)
                {
                    puan += 20;
                }
                kriterSayisi++;
            }

            // Kasa tipi puanı (20 puan)
            if (!string.IsNullOrEmpty(model.KasaTipi))
            {
                if (arac.KasaTipi == model.KasaTipi)
                {
                    puan += 20;
                }
                kriterSayisi++;
            }

            // Özel isteklere göre puan (20 puan)
            if (!string.IsNullOrEmpty(model.OzelIstekler))
            {
                puan += OzelIstekleriDegerlendir(arac, model.OzelIstekler);
                kriterSayisi++;
            }

            return puan;
        }

        private double OzelIstekleriDegerlendir(Entity.Arac arac, string ozelIstekler)
        {
            if (string.IsNullOrEmpty(ozelIstekler)) return 0;

            double puan = 0;
            var istekler = ozelIstekler.ToLower().Split(new[] { ',', '.', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var istek in istekler)
            {
                // Motor hacmi istekleri
                if (istek.Contains("motor") || istek.Contains("cc") || istek.Contains("hacim"))
                {
                    var istenenHacim = Regex.Match(istek, @"\d+").Value;
                    if (int.TryParse(istenenHacim, out int hacim))
                    {
                        var fark = Math.Abs(arac.MotorHacmi - hacim);
                        if (fark <= 100) puan += 2;
                        else if (fark <= 200) puan += 1;
                    }
                }

                // Beygir gücü istekleri
                if (istek.Contains("beygir") || istek.Contains("hp") || istek.Contains("güç"))
                {
                    var istenenGuc = Regex.Match(istek, @"\d+").Value;
                    if (int.TryParse(istenenGuc, out int guc))
                    {
                        var fark = Math.Abs(arac.BeygirGucu - guc);
                        if (fark <= 10) puan += 2;
                        else if (fark <= 20) puan += 1;
                    }
                }

                // Yakıt tüketimi istekleri
                if (istek.Contains("tüketim") || istek.Contains("yakıt") || istek.Contains("litre"))
                {
                    var istenenTuketim = Regex.Match(istek, @"\d+").Value;
                    if (double.TryParse(istenenTuketim, out double tuketim))
                    {
                        var fark = Math.Abs(arac.YakitTuketimi - tuketim);
                        if (fark <= 0.5) puan += 2;
                        else if (fark <= 1) puan += 1;
                    }
                }

                // Renk istekleri
                if (istek.Contains("renk"))
                {
                    var istenenRenk = istek.Replace("renk", "").Trim();
                    if (arac.Renk.ToLower().Contains(istenenRenk))
                        puan += 2;
                }

                // Kilometre istekleri
                if (istek.Contains("km") || istek.Contains("kilometre"))
                {
                    var istenenKm = Regex.Match(istek, @"\d+").Value;
                    if (int.TryParse(istenenKm, out int km))
                    {
                        var fark = Math.Abs(arac.Kilometre - km);
                        if (fark <= 10000) puan += 2;
                        else if (fark <= 20000) puan += 1;
                    }
                }

                // Genel özellik istekleri
                if (istek.Contains("otomatik") && arac.VitesTipi.ToLower().Contains("otomatik"))
                    puan += 2;
                if (istek.Contains("manuel") && arac.VitesTipi.ToLower().Contains("manuel"))
                    puan += 2;
                if (istek.Contains("dizel") && arac.YakitTipi.ToLower().Contains("dizel"))
                    puan += 2;
                if (istek.Contains("benzin") && arac.YakitTipi.ToLower().Contains("benzin"))
                    puan += 2;
                if (istek.Contains("hatchback") && arac.KasaTipi.ToLower().Contains("hatchback"))
                    puan += 2;
                if (istek.Contains("sedan") && arac.KasaTipi.ToLower().Contains("sedan"))
                    puan += 2;
                if (istek.Contains("suv") && arac.KasaTipi.ToLower().Contains("suv"))
                    puan += 2;
            }

            return puan;
        }

        public async Task<List<Entity.Arac>> AracOneriGetir(AracOneriViewModel model)
        {
            var araclar = await _context.Araclar
                .Include(a => a.Marka)
                .Where(a => a.SatistaMi) // Sadece satışta olan araçları getir
                .ToListAsync();

            var filtreliAraclar = araclar;

            // Fiyat aralığı filtresi
            if (!string.IsNullOrEmpty(model.FiyatAraligi))
            {
                try
                {
                    var (minFiyat, maxFiyat) = ParseFiyatAraligi(model.FiyatAraligi);
                    filtreliAraclar = filtreliAraclar.Where(a => a.Fiyat >= minFiyat && a.Fiyat <= maxFiyat).ToList();
                }
                catch (Exception)
                {
                    // Fiyat aralığı parse edilemezse filtreleme yapma
                }
            }

            // Yakıt tipi filtresi
            if (!string.IsNullOrEmpty(model.YakitTipi))
            {
                filtreliAraclar = filtreliAraclar.Where(a => a.YakitTipi == model.YakitTipi).ToList();
            }

            // Vites tipi filtresi
            if (!string.IsNullOrEmpty(model.VitesTipi))
            {
                filtreliAraclar = filtreliAraclar.Where(a => a.VitesTipi == model.VitesTipi).ToList();
            }

            // Model yılı filtresi
            if (!string.IsNullOrEmpty(model.ModelYili))
            {
                if (int.TryParse(model.ModelYili, out int modelYili))
                {
                    filtreliAraclar = filtreliAraclar.Where(a => a.ModelYili == modelYili).ToList();
                }
            }

            // Kasa tipi filtresi
            if (!string.IsNullOrEmpty(model.KasaTipi))
            {
                filtreliAraclar = filtreliAraclar.Where(a => a.KasaTipi == model.KasaTipi).ToList();
            }

            // Eğer hiç filtre uygulanmadıysa tüm araçları döndür
            if (filtreliAraclar.Count == 0)
            {
                return araclar;
            }

            // Puanlama sistemi
            var puanliAraclar = filtreliAraclar.Select(a => new
            {
                Arac = a,
                Puan = HesaplaPuan(a, model)
            })
            .Where(x => x.Puan > 0) // 0 puanlı araçları filtrele
            .OrderByDescending(x => x.Puan)
            .ToList();

            return puanliAraclar.Select(x => x.Arac).ToList();
        }
    }
} 